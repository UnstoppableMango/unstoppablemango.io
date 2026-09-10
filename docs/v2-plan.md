# V2 plan

The v2 theme is settled enough to build on.
This is the path from a single showcase page to a design system, a component library, and a rebuilt site with v1 retired.

Phases are dependency ordered.
Each one ends in something usable on its own.

## Decisions already made

The component catalog is a page in the app, not Storybook.
A catalog page renders every component from a registry, needs no second toolchain, and ships with the site.
Storybook would mean writing stories in JavaScript against Fable generated modules and maintaining a parallel build.

The theme switcher is public, on the real site, with the choice persisted.
A visitor facing switcher is what makes the readable mode worth building.

Palette and intensity are separate axes.
The professional variant and the accessibility mode do the same thing to the interface, so they are one axis with three stops rather than two features:

- `hud`, the current look
- `clean`, the professional variant: no glitch, minimal glow, solid surfaces
- `readable`, clean plus contrast floors, larger type, and motion off

Every palette is reachable at every intensity.

## Phase 0: name the design system

Done.
The design system is **Pulp**.

Modules live under `Pulp`, CSS variables take the `--pulp-` prefix, and palette classes read `.pulp-theme-slate`.

The runner up was Visor, which named the glass and the HUD directly.
Pulp won because the intensity axis makes the HUD one mode out of three, and a name describing the glass would misname the system at `clean` and `readable`.
Pulp names the tone and the brand instead, neither of which changes when the visual language does.

## Phase 1: theme primitives

Roughly half a day.

1. Move the `:root` block out of `src/app.css` into a themes file, with each palette a class scoped variable block (`.pulp-theme-slate`, `.pulp-theme-ink`, and so on).
2. Complete the colour families, so every one has the `dim` and `bright` steps the pairings turned out to need.
3. Name the remaining literals in `V2.fs`: the `bg-black/*` hover fills, the `#ffffff2e` inset highlights, and the arbitrary glow values.

CSS review and trim happens here, as part of the same audit.
Drop the unused config colours (`byzantium`, `thistle`, `cool-gray`, `cyber-pink`, `ember`), the backdrop leftovers from the layers that came out (`v2-wash`), and collapse duplicated arbitrary values into tokens.

## Phase 2: theme switching

Roughly a day.
Depends on phase 1.

1. Swap palettes by setting a class on the root element, with a Sutil store holding the choice and `localStorage` persisting it.
2. Add the intensity axis on the same mechanism.
3. Honour `prefers-reduced-motion` and `prefers-contrast` as the initial intensity, so the default is right before anyone touches the switcher.
4. Build the switcher UI.

The switcher needs somewhere to live in the site chrome, which pulls the nav component forward out of phase 3.

## Phase 3: component library

Roughly two days.
Depends on phase 1.

Structural review and trim comes first, before any extraction: module layout, the overlap between `Navigable` and `Components`, the webpack and Fable configuration, the generated `*.fs.js` build path, and the unused assets in `public/images`.
Reviewing first means extracting into the structure you want rather than the one you have.

1. Extract the components from `V2.fs` into their own modules, leaving `V2.fs` as the showcase.
2. Build the catalog page from a component registry, so adding a component adds its entry.
3. Add the components the site needs: nav and header, card, table, modal, tabs, toast, and code block.

## Phase 4: the v2 site

Two to three days.
Depends on phase 3.

1. Slideshow backdrop: cross fade the photos in `public/images`, preload the next frame, and hold still under `prefers-reduced-motion`.
2. Rebuild Home, Music, Artists, Cannes, and Wishlist on the library.

## Phase 5: v1 retirement

Half a day.
Depends on phase 4.

1. Redirect the v1 hash routes to their v2 equivalents.
2. Delete `src/App/pages/v1/`.
3. Remove whatever config and assets only v1 referenced.
