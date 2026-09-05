module V2

open Browser
open Sutil
open Sutil.Core
open Sutil.CoreElements

// ── Shared helpers ────────────────────────────────────────────────────────────

/// Corner-bracket HUD frame rendered with CSS border accent divs.
let private hudFrame (extraClasses: string) children =
    Html.divc $"relative rounded-lg border border-glass-edge bg-glass-fill shadow-v2-panel {extraClasses}" [
        Html.divc "absolute top-0 left-0 w-3 h-3 border-t-2 border-l-2 border-primary" []
        Html.divc "absolute top-0 right-0 w-3 h-3 border-t-2 border-r-2 border-primary" []
        Html.divc "absolute bottom-0 left-0 w-3 h-3 border-b-2 border-l-2 border-primary" []
        Html.divc "absolute bottom-0 right-0 w-3 h-3 border-b-2 border-r-2 border-primary" []
        yield! children
    ]

// ── Components ────────────────────────────────────────────────────────────────

/// Shared chassis for the button family: clipped corners, thin wide type, and a
/// lit edge marker that grows to full height on hover. Callers supply colour.
let private angularButton (tone: string) (marker: string) (label: string) =
    // Plain concatenation, not interpolation: the percent signs in the clip-path
    // read as printf format specifiers inside an interpolated string.
    let chassis =
        "group relative rounded-none pl-7 pr-6 py-2.5 \
         font-mono font-light uppercase text-[11px] tracking-[0.28em] \
         [clip-path:polygon(9px_0,100%_0,100%_calc(100%-9px),calc(100%-9px)_100%,0_100%,0_9px)] \
         transition-all duration-100 "

    Html.buttonc (chassis + tone) [
        Html.divc $"absolute left-0 top-[9px] bottom-0 w-[4px] transition-all duration-100 \
             group-hover:top-0 {marker}" []
        text label
    ]

/// Primary CTA — filled and lit, the one action the page wants you to take.
let cyberButton (label: string) =
    angularButton
        "text-white bg-primary-dim/70 border border-primary-bright \
         shadow-[0_0_20px_var(--pulp-primary-glow),inset_0_1px_0_var(--pulp-glass-rim)] \
         drop-shadow-[0_0_10px_var(--pulp-primary-glow)] \
         hover:bg-glass-press hover:border-primary-lift \
         hover:shadow-[0_0_32px_var(--pulp-primary-bright-glow),inset_0_1px_0_var(--pulp-glass-rim-bright)] \
         active:bg-glass-press-deep"
        "bg-lit shadow-[0_0_12px_var(--pulp-primary-lift),0_0_24px_var(--pulp-primary-bright)] \
         group-hover:shadow-[0_0_18px_var(--pulp-lit),0_0_36px_var(--pulp-primary-bright)]"
        label

/// Ghost / outline variant — same chassis, unfilled until you reach for it.
let cyberButtonGhost (label: string) =
    angularButton
        "text-primary-bright bg-primary/10 border border-primary-bright/50 \
         hover:bg-glass-press hover:text-primary-lift hover:border-primary-lift \
         hover:shadow-[0_0_18px_var(--pulp-primary-glow)] \
         active:bg-glass-press-deep"
        "bg-primary-bright/70 group-hover:bg-lit \
         group-hover:shadow-[0_0_14px_var(--pulp-primary-lift)]"
        label

/// Destructive variant — the danger colour rather than the primary, otherwise
/// identical.
let cyberButtonDanger (label: string) =
    angularButton
        "text-danger-bright bg-danger/15 border border-danger/60 \
         hover:bg-glass-press hover:text-danger-bright hover:border-danger-bright \
         hover:shadow-[0_0_20px_var(--pulp-danger-glow)] \
         active:bg-glass-press-deep"
        "bg-danger-bright/80 group-hover:bg-lit \
         group-hover:shadow-[0_0_14px_var(--pulp-danger-bright)]"
        label

/// Monospaced heading with neon glow.
let cyberHeading (level: int) (label: string) =
    let sizeClass =
        match level with
        | 1 -> "text-4xl"
        | 2 -> "text-2xl"
        | 3 -> "text-xl"
        | _ -> "text-base"

    Html.divc $"font-mono font-normal uppercase tracking-widest text-white {sizeClass} \
          drop-shadow-[0_0_5px_var(--pulp-primary-glow)] animate-hud-appear" [
        Html.spanc "text-accent" [
            text "// "
        ]
        text label
    ]

/// Status badge — mimics ammo / health counter readouts.
let hudBadge (label: string) (value: string) =
    Html.divc "flex flex-col items-center gap-0.5" [
        Html.spanc "font-mono text-xs text-white/40 uppercase tracking-widest" [
            text label
        ]
        Html.spanc "font-mono text-2xl font-normal text-white tabular-nums leading-none" [
            text value
        ]
    ]

/// Thin neon horizontal divider.
let neonDivider () =
    Html.divc "w-full h-px bg-gradient-to-r from-transparent via-primary to-transparent opacity-60" []

/// Input field: recessed fill, thin primary border on focus.
let cyberInput (placeholder: string) =
    Html.inputc "w-full rounded-lg bg-glass-well border border-glass-edge text-white font-mono text-sm px-3 py-2 \
         shadow-v2-inset \
         placeholder-white/25 \
         focus:outline-none focus:border-primary focus:shadow-[inset_0_0_4px_var(--pulp-primary-glow-soft)] \
         transition-all duration-150" [
        Attr.placeholder placeholder
    ]

/// Tag / chip component.
///
/// Two corrections keep the label centred rather than riding high and left.
/// All-caps text has no descenders, so the top padding is a pixel heavier than
/// the bottom to offset the descender space the line box reserves anyway.
/// `text-box: trim-both cap alphabetic` is the mechanism aimed at that problem,
/// but it lands a shade high with this font, so the padding carries it and
/// behaves the same in every browser.
///
/// The end padding subtracts the letter-spacing `tracking-wider` adds after the
/// final character, which would otherwise push the label left of centre.
let cyberTag (label: string) =
    Html.spanc "inline-flex items-center leading-none rounded-full \
         pl-2 pt-[4px] pb-[2px] [padding-inline-end:calc(0.5rem_-_0.05em)] \
         border border-accent/50 bg-glass-well \
         text-accent-bright font-mono text-xs uppercase tracking-wider" [
        text label
    ]

/// Notification / alert card.
let hudAlert (kind: string) (msg: string) =
    let kindClass =
        match kind with
        | "warn" -> "text-warn border-warn/30"
        | "ok" -> "text-ok border-ok/30"
        | "err" -> "text-danger border-danger/40"
        | _ -> "text-primary border-primary/30"

    Html.divc $"flex items-start gap-3 p-3 rounded-lg border bg-glass-fill shadow-v2-inset {kindClass}" [
        Html.spanc "font-mono text-xs font-normal uppercase tracking-widest mt-0.5 shrink-0" [
            text $"[{kind}]"
        ]
        Html.spanc "font-mono text-xs text-white/80" [
            text msg
        ]
    ]

/// Thin neon progress bar.
let hudProgress (pct: int) =
    Html.divc "w-full h-1.5 rounded-full bg-glass-track relative overflow-hidden" [
        Html.divc "h-full rounded-full bg-accent shadow-[0_0_4px_var(--pulp-accent)]" [
            Attr.style $"width: {pct}%%"
        ]
    ]

// ── Palette switcher ──────────────────────────────────────────────────────────

/// One empty frame per palette, marked in the corners with the colours it
/// applies. Only the palettes you can switch to appear, so there is no active
/// state to draw: the trigger names the one in force.
///
/// The frame has to show a palette it is not applying, so the colours come from
/// the palette record as inline styles rather than from the theme variables,
/// which always describe the palette currently in force.
///
/// The brackets sit on the corners the chassis clip-path leaves intact: it
/// chamfers the top left and bottom right, so the top right and bottom left are
/// the two square corners left to mark.
///
/// The group is named. An anonymous `group` here would also answer to the
/// switcher's own group wrapping it, and hovering anywhere on the control would
/// light every frame at once.
let private paletteChip (palette: Pulp.Theme.Palette) =
    Html.buttonc (
        "group/chip relative block h-12 rounded-none bg-transparent \
         border border-glass-edge/25 hover:border-glass-edge/60 transition-all duration-100 "
        + "[clip-path:polygon(7px_0,100%_0,100%_calc(100%-7px),calc(100%-7px)_100%,0_100%,0_7px)]"
    ) [
        Attr.title palette.Label
        Attr.custom ("aria-label", $"Theme: {palette.Label}")
        onClick (fun _ -> Pulp.Theme.select palette) []

        // The interior is empty until the pointer arrives, then fills with the
        // two colours meeting at a hard edge. A blend between them read as one
        // muddy third colour and said nothing about either; two flat fields at
        // low alpha state the pairing and leave the contrast between them intact.
        Html.divc "absolute inset-0 opacity-0 group-hover/chip:opacity-100 transition-opacity duration-150" [
            Attr.style
                $"background: linear-gradient(90deg, {palette.Primary}59 0 62%%, {palette.Accent}59 62%% 100%%)"
        ]

        Html.divc "absolute top-0 right-0 w-6 h-6 border-t-2 border-r-2" [
            Attr.style $"border-color: {palette.Primary}b3"
        ]
        Html.divc "absolute bottom-0 left-0 w-6 h-6 border-b-2 border-l-2" [
            Attr.style $"border-color: {palette.Accent}b3"
        ]
    ]

/// The collapsed state: the word, in the same bracketed mono the alerts use, and
/// no frame at all. It names the current palette rather than drawing it, since
/// the set is one hover away.
let private paletteTrigger (active: Pulp.Theme.Palette) =
    // A button rather than a div, so the keyboard has somewhere to land. The set
    // opens on focus, which means clicking the trigger opens it too and no click
    // handler is needed.
    Html.buttonc "font-mono text-sm uppercase tracking-[0.25em] text-white/35 \
         group-hover:text-white/70 focus-visible:text-white/70 transition-colors duration-150 \
         select-none bg-transparent border-none p-0 text-left" [
        Attr.custom ("type", "button")
        Attr.custom ("aria-haspopup", "true")
        Attr.title $"Theme: {active.Label}"
        text $"[ {active.Label} ]"
    ]

/// Palette switcher: collapsed to the frame of the palette in force, expanding
/// upward into the full set on hover. Each frame is empty and unpanelled, so the
/// control reads as instrument marks on the glass rather than a widget.
///
/// The column and the trigger share one hover region, so the pointer can travel
/// from one to the other without the set collapsing underneath it.
/// `group-focus-within` opens the same way for the keyboard, so focusing the
/// trigger reveals the set before the tab order reaches it.
///
/// It sits with the other page level controls for now; the nav component takes
/// it over once there is real site chrome.
let private paletteSwitcher () =
    // The column takes its width from its widest child, which is the trigger
    // label, and the frames stretch to match it. So the set is exactly as wide
    // as the word naming the current palette, and it resizes when that word
    // changes length.
    Html.divc "group fixed bottom-6 left-6 z-[60] flex flex-col-reverse items-stretch gap-2 w-fit" [
        Bind.el (
            Pulp.Theme.current,
            fun active ->
                fragment [
                    paletteTrigger active

                    Html.divc "flex flex-col-reverse items-stretch gap-2 \
                         opacity-0 translate-y-2 pointer-events-none \
                         group-hover:opacity-100 group-hover:translate-y-0 group-hover:pointer-events-auto \
                         group-focus-within:opacity-100 group-focus-within:translate-y-0 group-focus-within:pointer-events-auto \
                         transition-all duration-200" [
                        // The active palette is named by the trigger, so the
                        // column offers only the alternatives.
                        for p in Pulp.Theme.palettes do
                            if p.Name <> active.Name then
                                paletteChip p
                    ]
                ]
        )
    ]

// ── Private section wrapper ───────────────────────────────────────────────────

let private section (title: string) children =
    Html.divc "flex flex-col gap-4" [
        cyberHeading 2 title
        neonDivider ()
        yield! children
    ]

// ── Glitch title ──────────────────────────────────────────────────────────────

let private glitchTitle () =
    Html.divc "relative inline-block select-none" [
        Html.spanc "font-mono font-light uppercase text-5xl text-white tracking-[0.2em] \
             drop-shadow-[0_0_12px_var(--pulp-primary)] animate-flicker block" [
            text "V2 THEME"
        ]
        Html.spanc "absolute inset-0 font-mono font-light uppercase text-5xl text-primary \
             tracking-[0.2em] opacity-70 animate-glitch block pointer-events-none" [
            Attr.style "clip-path: inset(15% 0 75% 0)"
            text "V2 THEME"
        ]
        Html.spanc "absolute inset-0 font-mono font-light uppercase text-5xl text-accent \
             tracking-[0.2em] opacity-40 animate-glitch-clip block pointer-events-none" [
            Attr.style "clip-path: inset(65% 0 10% 0); transform: translate(3px)"
            text "V2 THEME"
        ]
    ]

// ── Animation demo tiles ──────────────────────────────────────────────────────

/// Restarts the tile's animation. The class has to come off, the browser has to
/// recalculate styles, and only then can it go back on — hence the two frames.
let private replayAnimation (animClass: string) (ev: Browser.Types.Event) =
    let tile = ev.currentTarget :?> Browser.Types.HTMLElement
    let target = tile.querySelector ".anim-target" :?> Browser.Types.HTMLElement

    if not (isNull (box target)) then
        let original = target.className
        target.className <- original.Replace(animClass, "")

        window.requestAnimationFrame (fun _ ->
            window.requestAnimationFrame (fun _ -> target.className <- original) |> ignore)
        |> ignore

/// Box preview — for animations that transform, scale or glow.
let private animBox (animClass: string) =
    Html.divc $"anim-target w-9 h-9 border-2 border-primary bg-primary/10 {animClass}" []

/// Type preview — for animations that move letter-spacing, clip-path or opacity,
/// which a bare box barely registers.
let private animType (animClass: string) =
    Html.spanc $"anim-target font-mono font-light uppercase text-xl tracking-[0.2em] text-white \
         drop-shadow-[0_0_6px_var(--pulp-primary)] {animClass}" [
        text "V2"
    ]

/// Sweep preview — the scan line, scoped to the tile instead of the viewport.
let private animSweep (animClass: string) =
    Html.divc $"anim-target absolute left-0 right-0 h-0.5 bg-primary shadow-[0_0_6px_var(--pulp-primary)] {animClass}" []

let private animTile (name: string) (animClass: string) (loops: bool) preview =
    hudFrame "group cursor-pointer select-none p-3 flex flex-col items-center gap-2 \
         hover:border-primary/40 transition-colors" [
        onClick (replayAnimation animClass) []
        Html.divc "relative w-full h-14 flex items-center justify-center overflow-hidden" [
            preview
        ]
        Html.spanc "font-mono text-xs text-white/40 uppercase tracking-widest" [
            text name
        ]
        Html.spanc "font-mono text-[10px] uppercase tracking-widest text-white/0 \
             group-hover:text-primary transition-colors" [
            text (if loops then "replay // loops" else "replay // once")
        ]
    ]

// ── Main view ─────────────────────────────────────────────────────────────────

let view () =
    // The backdrop layers live outside the animated wrapper on purpose:
    // animate-power-on leaves a transform and filter on its element, which would
    // turn that element into the containing block for `fixed` children and drag
    // the backdrop along with the scroll.
    Html.divc "relative min-h-screen text-white font-mono overflow-y-auto" [
        // Backdrop: the blurred photo, pinned to the viewport so it holds still
        // while the HUD scrolls over it.
        Html.divc "pointer-events-none fixed inset-0 z-0 overflow-hidden" [
            // Pre-blurred at build time: a 960px blurred frame is a fraction of
            // the sharp original's bytes and saves a live backdrop-filter.
            Html.divc "absolute inset-0 bg-cover bg-center bg-[url(images/matobo-blur.webp)]" []
        ]

        // Thick glass pane between the desert ground and the HUD content.
        Html.divc "pointer-events-none fixed inset-0 z-0 \
             bg-v2-glass shadow-v2-pane" []
        Html.ac
            "fixed top-4 right-4 z-[60] inline-flex items-center justify-center border border-primary bg-glass-slab px-4 py-2 text-xs font-normal uppercase tracking-widest text-primary shadow-[0_0_12px_var(--pulp-primary-glow-soft)] transition hover:bg-primary/10 lg:hidden"
            [
                Attr.href "#/"
                text "EXIT PREVIEW"
            ]

        paletteSwitcher ()

        // Content column: a darker translucent slab running the full height of
        // the page, with the backdrop left visible in the margins either side.
        Html.divc "relative z-10 max-w-4xl mx-auto px-6 sm:px-10 py-12 flex flex-col gap-12 \
             bg-glass-slab border-x border-glass-edge/40 shadow-v2-column \
             animate-power-on" [

            // ── Hero ─────────────────────────────────────────────────────────
            Html.divc "flex flex-col items-center gap-4 py-8" [
                Html.divc $"text-xs text-primary uppercase tracking-[0.4em] mb-2 animate-hud-appear" [
                    text "SYSTEM v2.0.0 // CYBERPUNK UI KIT"
                ]
                glitchTitle ()
                Html.divc "flex gap-6 mt-4" [
                    hudBadge "COMPONENTS" "12"
                    Html.divc "w-px bg-lit/10" []
                    hudBadge "ANIMATIONS" "7"
                    Html.divc "w-px bg-lit/10" []
                    hudBadge "STATUS" "OK"
                ]
            ]

            neonDivider ()

            // ── Buttons ───────────────────────────────────────────────────────
            section "BUTTONS" [
                Html.divc "flex flex-wrap gap-4 items-center" [
                    cyberButton "EXECUTE"
                    cyberButtonGhost "SCAN"
                    cyberButtonDanger "ABORT"
                ]
            ]

            // ── Typography ────────────────────────────────────────────────────
            section "TYPOGRAPHY" [
                cyberHeading 1 "ALPHA SECTOR"
                cyberHeading 2 "BRAVO SECTOR"
                cyberHeading 3 "CHARLIE SECTOR"
                Html.pc "text-sm text-white/60 font-mono leading-relaxed" [
                    text "Auxiliary data stream active. Signal integrity nominal. Monitoring frequency 2.4 GHz."
                ]
            ]

            // ── Tags ──────────────────────────────────────────────────────────
            section "TAGS" [
                Html.divc "flex flex-wrap gap-2" [
                    for t in
                        [
                            "STEALTH"
                            "HOSTILE"
                            "TRACKED"
                            "ANOMALY"
                            "LINKED"
                        ] do
                        cyberTag t
                ]
            ]

            // ── Inputs ────────────────────────────────────────────────────────
            section "INPUTS" [
                cyberInput "SEARCH OPERATOR..."
                cyberInput "AUTH TOKEN //"
            ]

            // ── HUD Panels ────────────────────────────────────────────────────
            section "HUD PANELS" [
                Html.divc "grid grid-cols-1 md:grid-cols-2 gap-4" [
                    hudFrame "p-6" [
                        Html.divc "font-mono text-xs text-primary uppercase tracking-widest mb-3" [
                            text "// OPERATOR STATUS"
                        ]
                        Html.divc "flex flex-col gap-2" [
                            Html.divc "flex justify-between" [
                                Html.spanc "text-white/40 text-xs" [
                                    text "HEALTH"
                                ]
                                Html.spanc "text-white text-xs" [
                                    text "87%"
                                ]
                            ]
                            hudProgress 87
                            Html.divc "flex justify-between mt-1" [
                                Html.spanc "text-white/40 text-xs" [
                                    text "ARMOR"
                                ]
                                Html.spanc "text-white text-xs" [
                                    text "52%"
                                ]
                            ]
                            hudProgress 52
                            Html.divc "flex justify-between mt-1" [
                                Html.spanc "text-white/40 text-xs" [
                                    text "SIGNAL"
                                ]
                                Html.spanc "text-white text-xs" [
                                    text "100%"
                                ]
                            ]
                            hudProgress 100
                        ]
                    ]
                    hudFrame "p-6" [
                        Html.divc "font-mono text-xs text-primary uppercase tracking-widest mb-3" [
                            text "// AMMO COUNTER"
                        ]
                        Html.divc "flex gap-6 items-end" [
                            hudBadge "ROUNDS" "024"
                            hudBadge "RESERVE" "096"
                            hudBadge "MODE" "AP"
                        ]
                    ]
                ]
            ]

            // ── Alerts ────────────────────────────────────────────────────────
            section "ALERTS" [
                hudAlert "err" "Firewall breach detected on subnet 10.0.4.1 — isolating node."
                hudAlert "warn" "Signal degradation on channel 7. Rerouting."
                hudAlert "ok" "Authentication handshake successful. Access granted."
            ]

            // ── Animation showcase ────────────────────────────────────────────
            // Every tile runs its animation live; click one to replay it. The
            // one-shot animations need that, since they otherwise only ever play
            // on mount.
            section "ANIMATIONS" [
                Html.divc "grid grid-cols-2 md:grid-cols-4 gap-4 text-center" [
                    animTile "GLITCH" "animate-glitch" true (animType "animate-glitch")
                    animTile "GLITCH 2" "animate-glitch-clip" true (animType "animate-glitch-clip")
                    animTile "FLICKER" "animate-flicker" true (animType "animate-flicker")
                    animTile "PULSE" "animate-pulse-primary" true (animBox "animate-pulse-primary")
                    animTile "SCAN LINE" "animate-scan-line" true (animSweep "animate-scan-line")
                    animTile "HUD IN" "animate-hud-appear" false (animType "animate-hud-appear")
                    animTile "POWER ON" "animate-power-on" false (animBox "animate-power-on")
                    animTile "SLIDE DN" "animate-slide-down" false (animBox "animate-slide-down")
                ]
            ]

            // ── Footer ────────────────────────────────────────────────────────
            neonDivider ()
            Html.divc "flex justify-between items-center py-2 text-xs text-white/20 font-mono" [
                text "// V2 THEME PREVIEW"
                text "UNSTOPPABLEMANGO.IO"
                text "EOF //"
            ]
        ]
    ]
