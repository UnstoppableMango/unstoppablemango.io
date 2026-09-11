/// Pulp's two theme axes: which colour the interface is, and how loud.
///
/// Both are a class on the root element and nothing more. The CSS under
/// src/pulp defines what each class means, so this module never knows a colour
/// value or a shadow; it knows names, which one is current, and how to remember
/// the choice.
module Pulp.Theme

open Browser
open Sutil

/// The keys and prefixes the no-flash script in index.html reads. Both sides
/// have to agree on them, and the script runs before this module exists, so the
/// strings are repeated there on purpose.
let private paletteKey = "pulp-theme"
let private palettePrefix = "pulp-theme-"

let private intensityKey = "pulp-intensity"
let private intensityPrefix = "pulp-intensity-"

// ── The shared mechanism ─────────────────────────────────────────────────────

/// Storage throws rather than returning null when a browser blocks site data,
/// so every read and write is guarded. So is `matchMedia`, which some
/// environments leave undefined.
let private guarded fallback f =
    try
        f ()
    with _ ->
        fallback

let private readStored key byName =
    guarded None (fun () -> localStorage.getItem key |> Option.ofObj |> Option.bind byName)

let private writeStored key (name: string) =
    guarded () (fun () -> localStorage.setItem (key, name))

let private matches (query: string) =
    guarded false (fun () -> window.matchMedia(query).matches)

/// Swaps one axis' class on the root element, leaving every other class alone.
/// The two axes share this element, so neither may clear the list wholesale.
///
/// Every class carrying the prefix goes, not only the ones this module knows.
/// The no-flash script adds `<prefix><name>` for any lowercase storage value, so
/// a renamed or removed option can leave a class behind. Splitting the string
/// first gives a snapshot, so the removals do not walk a list they are mutating.
let private applyClass (prefix: string) (name: string) =
    let root = document.documentElement

    for c in root.className.Split(' ') do
        if c.StartsWith prefix then
            root.classList.remove c

    root.classList.add (prefix + name)

// ── The palette axis: which colour the interface is ──────────────────────────

type Palette = {
    /// The class suffix, and the value persisted to storage.
    Name: string
    /// How the palette is labelled in the switcher.
    Label: string
    /// Swatch colours, for a chip that shows the palette instead of naming it.
    Primary: string
    Accent: string
}

/// Every palette defined in src/pulp/palettes.css, in the order the switcher
/// shows them. The colours here duplicate the CSS, since a swatch has to paint
/// a palette that is not currently applied.
let palettes = [
    {
        Name = "slate"
        Label = "SLATE"
        Primary = "#7d8a99"
        Accent = "#c4553f"
    }
    {
        Name = "ink"
        Label = "INK"
        Primary = "#4a5a8c"
        Accent = "#c9a227"
    }
    {
        Name = "olive"
        Label = "OLIVE"
        Primary = "#7a8b5a"
        Accent = "#d9a441"
    }
    {
        Name = "rust"
        Label = "RUST"
        Primary = "#b4644a"
        Accent = "#8fb4d0"
    }
    {
        Name = "uni"
        Label = "UNI"
        Primary = "#4b116f"
        Accent = "#ffcc00"
    }
    {
        Name = "uwp"
        Label = "UWP"
        Primary = "#1a64b7"
        Accent = "#f58113"
    }
]

let defaultPalette = palettes.Head

let private paletteByName name =
    palettes |> List.tryFind (fun p -> p.Name = name)

/// The current palette. Seeded from storage so a reload keeps the choice, and
/// falling back to the default, which is also what :root defines.
let currentPalette =
    Store.make (readStored paletteKey paletteByName |> Option.defaultValue defaultPalette)

let selectPalette (palette: Palette) =
    applyClass palettePrefix palette.Name
    writeStored paletteKey palette.Name
    Store.set currentPalette palette

// ── The intensity axis: how loud the interface is ────────────────────────────

type Intensity = {
    /// The class suffix, and the value persisted to storage.
    Name: string
    /// How the stop is labelled in the switcher.
    Label: string
    /// What the stop does, for the switcher's tooltip.
    Description: string
}

/// The three stops defined in src/pulp/intensity.css, quietest last.
let intensities = [
    {
        Name = "hud"
        Label = "HUD"
        Description = "The full instrument panel"
    }
    {
        Name = "clean"
        Label = "CLEAN"
        Description = "No glitch, minimal glow, solid surfaces"
    }
    {
        Name = "readable"
        Label = "READABLE"
        Description = "Clean, with contrast floors, larger type and motion off"
    }
]

let defaultIntensity = intensities.Head

let private intensityByName name =
    intensities |> List.tryFind (fun i -> i.Name = name)

/// What the operating system has already said, for a visitor who has never
/// touched the switcher. Both queries land on the same stop because the axis
/// has one accessibility stop rather than one per preference.
let private preferredIntensity () =
    if matches "(prefers-contrast: more)" || matches "(prefers-reduced-motion: reduce)" then
        intensityByName "readable" |> Option.defaultValue defaultIntensity
    else
        defaultIntensity

/// A stored choice outranks the system preference: the switcher is how a
/// visitor overrides it.
let currentIntensity =
    Store.make (readStored intensityKey intensityByName |> Option.defaultWith preferredIntensity)

let selectIntensity (intensity: Intensity) =
    applyClass intensityPrefix intensity.Name
    writeStored intensityKey intensity.Name
    Store.set currentIntensity intensity

// ── Startup ──────────────────────────────────────────────────────────────────

/// Called once at startup. The no-flash script has already set both classes, so
/// this only reconciles the stores with what the document is showing.
let init () =
    applyClass palettePrefix (Store.get currentPalette).Name
    applyClass intensityPrefix (Store.get currentIntensity).Name
