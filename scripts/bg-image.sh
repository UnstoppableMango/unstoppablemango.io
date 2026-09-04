#!/usr/bin/env bash
# Prepare a background photo for the site: sharp variants for direct use, plus a
# small pre-blurred frame that replaces a runtime backdrop-filter.
#
#   scripts/bg-image.sh ~/Downloads/DSC_0001.jpg matobo [sigma]
#
# Outputs, all under public/images:
#   <name>-lg.webp    1920px, quality 70
#   <name>-sm.webp    1024px, quality 70
#   <name>-blur.webp   960px, blurred, quality 60
#
# The blur sigma is expressed against the 960px frame, so it is roughly half the
# CSS pixel radius it stands in for at a 1920px viewport.
#
# Phone photos carry the camera make and model, capture timestamps, and often
# GPS coordinates. Outputs keep pixels and nothing else: -strip drops metadata,
# +profile removes every embedded profile, and each file is verified afterwards.
set -euo pipefail

readonly SHARP_QUALITY=70
readonly BLUR_QUALITY=60
readonly BLUR_WIDTH=960

# Groups that describe the encoding rather than the photographer: exiftool
# always reports these, and none of them travel from the source.
readonly ALLOWED_GROUPS='ExifTool|File|RIFF|Composite'

usage() {
	echo "usage: $(basename "$0") <source-image> <name> [blur-sigma]" >&2
	exit 2
}

# Fails if anything beyond the structural groups survived into the output.
verify_stripped() {
	local file=$1 leftovers

	if ! command -v exiftool >/dev/null 2>&1; then
		echo "warning: exiftool not found, cannot verify $(basename "$file")" >&2
		return 0
	fi

	leftovers=$(exiftool -G0 -s -a "$file" | grep -Ev "^\[($ALLOWED_GROUPS)\]" || true)

	if [ -n "$leftovers" ]; then
		echo "metadata survived in $file:" >&2
		echo "$leftovers" >&2
		return 1
	fi
}

[ $# -ge 2 ] || usage

src=$1
name=$2
sigma=${3:-3}

# The name is interpolated into every output path, so it has to stay a plain
# filename. A value carrying a separator would write variants outside public/images.
case "$name" in
"" | . | .. | */* | *\\*)
	echo "name must be a plain asset filename, with no path separators: $name" >&2
	exit 2
	;;
esac

[ -f "$src" ] || {
	echo "no such file: $src" >&2
	exit 1
}

if ! command -v magick >/dev/null 2>&1; then
	echo "magick not found; run this inside 'nix develop'" >&2
	exit 1
fi

root=$(cd "$(dirname "$0")/.." && pwd)
out="$root/public/images"
mkdir -p "$out"

# -auto-orient reads the EXIF rotation before -strip discards it.
magick "$src" -auto-orient -resize 1920x -strip +profile '*' \
	-quality "$SHARP_QUALITY" "$out/$name-lg.webp"
magick "$src" -auto-orient -resize 1024x -strip +profile '*' \
	-quality "$SHARP_QUALITY" "$out/$name-sm.webp"
magick "$src" -auto-orient -resize "${BLUR_WIDTH}x" -blur "0x$sigma" -strip +profile '*' \
	-quality "$BLUR_QUALITY" "$out/$name-blur.webp"

for variant in lg sm blur; do
	verify_stripped "$out/$name-$variant.webp"
done

ls -lh "$out/$name-lg.webp" "$out/$name-sm.webp" "$out/$name-blur.webp"
