module V2

open Browser
open Sutil
open Sutil.Core
open Sutil.CoreElements

// ── Shared helpers ────────────────────────────────────────────────────────────

/// Corner-bracket HUD frame rendered with CSS border accent divs.
let private hudFrame (extraClasses: string) children =
    Html.divc $"relative rounded-lg border border-white/30 bg-white/[0.13] \
         shadow-[0_16px_48px_#070b1080,inset_0_2px_0_#ffffff4d,inset_0_-2px_0_#ffffff1f] {extraClasses}" [
        Html.divc "absolute top-0 left-0 w-3 h-3 border-t-2 border-l-2 border-cyber-pink" []
        Html.divc "absolute top-0 right-0 w-3 h-3 border-t-2 border-r-2 border-cyber-pink" []
        Html.divc "absolute bottom-0 left-0 w-3 h-3 border-b-2 border-l-2 border-cyber-pink" []
        Html.divc "absolute bottom-0 right-0 w-3 h-3 border-b-2 border-r-2 border-cyber-pink" []
        yield! children
    ]

// ── Components ────────────────────────────────────────────────────────────────

/// Primary CTA button — solid hot-pink, uppercase, monospaced.
let cyberButton (label: string) =
    Html.buttonc "relative rounded-lg px-6 py-2 bg-cyber-pink text-black font-mono font-normal uppercase tracking-widest text-sm \
         border border-cyber-pink \
         hover:bg-transparent hover:text-cyber-pink hover:shadow-[0_0_12px_#ff2d78] \
         active:scale-95 transition-all duration-150 animate-pulse-pink" [
        text label
    ]

/// Ghost / outline button variant.
let cyberButtonGhost (label: string) =
    Html.buttonc "px-6 py-2 rounded-lg bg-white/[0.12] text-cyber-pink font-mono font-normal uppercase tracking-widest text-sm \
         border border-cyber-pink \
         hover:bg-cyber-pink/10 hover:shadow-[0_0_8px_#ff2d78] \
         active:scale-95 transition-all duration-150" [
        text label
    ]

/// Danger / destructive button.
let cyberButtonDanger (label: string) =
    Html.buttonc "px-6 py-2 rounded-lg bg-white/[0.12] text-white font-mono font-normal uppercase tracking-widest text-sm \
         border border-white/30 \
         hover:border-cyber-pink hover:text-cyber-pink \
         active:scale-95 transition-all duration-150" [
        text label
    ]

/// Monospaced heading with neon glow.
let cyberHeading (level: int) (label: string) =
    let sizeClass =
        match level with
        | 1 -> "text-4xl"
        | 2 -> "text-2xl"
        | 3 -> "text-xl"
        | _ -> "text-base"

    Html.divc $"font-mono font-normal uppercase tracking-widest text-white {sizeClass} \
          drop-shadow-[0_0_6px_#ff2d78] animate-hud-appear" [
        Html.spanc "text-cyber-pink" [
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
    Html.divc "w-full h-px bg-gradient-to-r from-transparent via-cyber-pink to-transparent opacity-60" []

/// Input field: dark fill, thin pink border on focus.
let cyberInput (placeholder: string) =
    Html.inputc "w-full rounded-lg bg-white/[0.12] border border-white/30 text-white font-mono text-sm px-3 py-2 \
         shadow-[inset_0_2px_0_#ffffff40] \
         placeholder-white/25 \
         focus:outline-none focus:border-cyber-pink focus:shadow-[inset_0_0_4px_#ff2d7844] \
         transition-all duration-150" [
        Attr.placeholder placeholder
    ]

/// Tag / chip component.
let cyberTag (label: string) =
    Html.spanc "inline-block px-2 py-0.5 rounded-full border border-cyber-pink/50 bg-white/[0.12] \
         text-cyber-pink font-mono text-xs uppercase tracking-wider" [
        text label
    ]

/// Notification / alert card.
let hudAlert (kind: string) (msg: string) =
    let kindClass =
        match kind with
        | "warn" -> "text-yellow-400 border-yellow-400/30"
        | "ok" -> "text-green-400 border-green-400/30"
        | _ -> "text-cyber-pink border-cyber-pink/30"

    Html.divc $"flex items-start gap-3 p-3 rounded-lg border bg-white/[0.13] \
         shadow-[inset_0_2px_0_#ffffff40] {kindClass}" [
        Html.spanc "font-mono text-xs font-normal uppercase tracking-widest mt-0.5 shrink-0" [
            text $"[{kind}]"
        ]
        Html.spanc "font-mono text-xs text-white/80" [
            text msg
        ]
    ]

/// Thin neon progress bar.
let hudProgress (pct: int) =
    Html.divc "w-full h-1.5 rounded-full bg-white/15 relative overflow-hidden" [
        Html.divc "h-full rounded-full bg-cyber-pink shadow-[0_0_4px_#ff2d78]" [
            Attr.style $"width: {pct}%%"
        ]
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
             drop-shadow-[0_0_12px_#ff2d78] animate-flicker block" [
            text "V2 THEME"
        ]
        Html.spanc "absolute inset-0 font-mono font-light uppercase text-5xl text-cyber-pink \
             tracking-[0.2em] opacity-70 animate-glitch block pointer-events-none" [
            Attr.style "clip-path: inset(15% 0 75% 0)"
            text "V2 THEME"
        ]
        Html.spanc "absolute inset-0 font-mono font-light uppercase text-5xl text-cyan-400 \
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
    Html.divc $"anim-target w-9 h-9 border-2 border-cyber-pink bg-cyber-pink/10 {animClass}" []

/// Type preview — for animations that move letter-spacing, clip-path or opacity,
/// which a bare box barely registers.
let private animType (animClass: string) =
    Html.spanc $"anim-target font-mono font-light uppercase text-xl tracking-[0.2em] text-white \
         drop-shadow-[0_0_6px_#ff2d78] {animClass}" [
        text "V2"
    ]

/// Sweep preview — the scan line, scoped to the tile instead of the viewport.
let private animSweep (animClass: string) =
    Html.divc $"anim-target absolute left-0 right-0 h-0.5 bg-cyber-pink shadow-[0_0_6px_#ff2d78] {animClass}" []

let private animTile (name: string) (animClass: string) (loops: bool) preview =
    hudFrame "group cursor-pointer select-none p-3 flex flex-col items-center gap-2 \
         hover:border-cyber-pink/40 transition-colors" [
        onClick (replayAnimation animClass) []
        Html.divc "relative w-full h-14 flex items-center justify-center overflow-hidden" [
            preview
        ]
        Html.spanc "font-mono text-xs text-white/40 uppercase tracking-widest" [
            text name
        ]
        Html.spanc "font-mono text-[10px] uppercase tracking-widest text-white/0 \
             group-hover:text-cyber-pink transition-colors" [
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
        // Backdrop, bottom to top: photo, warm wash, colour blobs, sand, grain.
        // Pinned to the viewport so it holds still while the HUD scrolls over it.
        Html.divc "pointer-events-none fixed inset-0 z-0 overflow-hidden" [
            // Pre-blurred at build time: a 960px blurred frame is a fraction of
            // the sharp original's bytes and saves a live backdrop-filter.
            Html.divc "absolute inset-0 bg-cover bg-center bg-[url(images/roadside-blur.webp)]" []
            Html.divc "absolute inset-0 \
                 bg-[radial-gradient(120%_80%_at_50%_0%,#6b604714_0%,#4a423426_45%,#2b271f40_100%)]" []
            Html.divc "absolute -top-32 -left-24 w-[38rem] h-[38rem] rounded-full bg-desert-sand/25 blur-3xl" []
            Html.divc "absolute top-1/3 -right-32 w-[34rem] h-[34rem] rounded-full bg-cyber-pink/15 blur-3xl" []
            Html.divc "absolute -bottom-40 left-1/4 w-[42rem] h-[30rem] rounded-full bg-desert-dust/50 blur-3xl" []

            // Wind-blown sand, then film grain: texture for the glass to work on.
            Html.divc
                "absolute inset-0 bg-[url(images/sand-drift.svg)] bg-cover bg-center opacity-70 mix-blend-soft-light"
                []
            Html.divc "absolute inset-0 bg-[url(images/grain.svg)] bg-repeat opacity-[0.12] mix-blend-overlay" []
        ]

        // Thick glass pane between the desert ground and the HUD content.
        Html.divc "pointer-events-none fixed inset-0 z-0 \
             bg-[linear-gradient(155deg,#e8f2f85c_0%,#9dbccc42_38%,#0d141a1a_100%)] \
             shadow-[inset_0_2px_0_#ffffff66,inset_0_-70px_120px_#0a0f1426]" []
        Html.ac
            "fixed top-4 right-4 z-[60] inline-flex items-center justify-center border border-cyber-pink bg-black/70 px-4 py-2 text-xs font-normal uppercase tracking-widest text-cyber-pink shadow-[0_0_12px_#ff2d7833] transition hover:bg-cyber-pink/10 lg:hidden"
            [
                Attr.href "#/"
                text "EXIT PREVIEW"
            ]

        Html.divc "relative z-10 max-w-4xl mx-auto px-6 py-12 flex flex-col gap-12 animate-power-on" [

            // ── Hero ─────────────────────────────────────────────────────────
            Html.divc "flex flex-col items-center gap-4 py-8" [
                Html.divc $"text-xs text-cyber-pink uppercase tracking-[0.4em] mb-2 animate-hud-appear" [
                    text "SYSTEM v2.0.0 // CYBERPUNK UI KIT"
                ]
                glitchTitle ()
                Html.divc "flex gap-6 mt-4" [
                    hudBadge "COMPONENTS" "12"
                    Html.divc "w-px bg-white/10" []
                    hudBadge "ANIMATIONS" "7"
                    Html.divc "w-px bg-white/10" []
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
                        Html.divc "font-mono text-xs text-cyber-pink uppercase tracking-widest mb-3" [
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
                        Html.divc "font-mono text-xs text-cyber-pink uppercase tracking-widest mb-3" [
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
                    animTile "PULSE" "animate-pulse-pink" true (animBox "animate-pulse-pink")
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
