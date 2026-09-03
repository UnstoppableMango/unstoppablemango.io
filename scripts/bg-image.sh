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
set -euo pipefail

readonly SHARP_QUALITY=70
readonly BLUR_QUALITY=60
readonly BLUR_WIDTH=960

usage() {
	echo "usage: $(basename "$0") <source-image> <name> [blur-sigma]" >&2
	exit 2
}

[ $# -ge 2 ] || usage

src=$1
name=$2
sigma=${3:-3}

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

magick "$src" -auto-orient -resize 1920x -strip -quality "$SHARP_QUALITY" "$out/$name-lg.webp"
magick "$src" -auto-orient -resize 1024x -strip -quality "$SHARP_QUALITY" "$out/$name-sm.webp"
magick "$src" -auto-orient -resize "${BLUR_WIDTH}x" -blur "0x$sigma" -strip \
	-quality "$BLUR_QUALITY" "$out/$name-blur.webp"

ls -lh "$out/$name-lg.webp" "$out/$name-sm.webp" "$out/$name-blur.webp"
