/// Pulp's palette axis: which colour the interface is.
///
/// A palette is a class on the root element and nothing more. The CSS under
/// src/pulp defines what each class means, so this module never knows a colour
/// value; it knows names, which one is current, and how to remember the choice.
module Pulp.Theme

open Browser
open Sutil

/// The key the no-flash script in index.html reads. Both sides have to agree on
/// it, and the script runs before this module exists, so the string is repeated
/// there on purpose.
let private storageKey = "pulp-theme"

let private classPrefix = "pulp-theme-"

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

let private byName name =
    palettes |> List.tryFind (fun p -> p.Name = name)

/// Storage throws rather than returning null when a browser blocks site data,
/// so every read and write is guarded.
let private readStored () =
    try
        localStorage.getItem storageKey |> Option.ofObj |> Option.bind byName
    with _ ->
        None

let private writeStored (palette: Palette) =
    try
        localStorage.setItem (storageKey, palette.Name)
    with _ ->
        ()

/// Swaps the palette class on the root element, leaving every other class alone.
/// The intensity axis will write its own class here, so this must not clear the
/// list wholesale.
let private applyClass (palette: Palette) =
    let root = document.documentElement

    for p in palettes do
        root.classList.remove (classPrefix + p.Name)

    root.classList.add (classPrefix + palette.Name)

/// The current palette. Seeded from storage so a reload keeps the choice, and
/// falling back to the default, which is also what :root defines.
let current = Store.make (readStored () |> Option.defaultValue defaultPalette)

let select (palette: Palette) =
    applyClass palette
    writeStored palette
    Store.set current palette

/// Called once at startup. The no-flash script has already set the class, so
/// this only reconciles the store with what the document is showing.
let init () = Store.get current |> applyClass
