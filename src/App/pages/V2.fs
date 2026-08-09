module V2

open Sutil
open Sutil.Core
open Sutil.CoreElements

// ── Shared helpers ────────────────────────────────────────────────────────────

/// Corner-bracket HUD frame rendered with CSS border accent divs.
let private hudFrame (extraClasses: string) children =
    Html.divc
        $"relative border border-white/10 bg-black/60 backdrop-blur-md {extraClasses}"
        [
            Html.divc "absolute top-0 left-0 w-3 h-3 border-t-2 border-l-2 border-cyber-pink" []
            Html.divc "absolute top-0 right-0 w-3 h-3 border-t-2 border-r-2 border-cyber-pink" []
            Html.divc "absolute bottom-0 left-0 w-3 h-3 border-b-2 border-l-2 border-cyber-pink" []
            Html.divc "absolute bottom-0 right-0 w-3 h-3 border-b-2 border-r-2 border-cyber-pink" []
            yield! children
        ]

// ── Components ────────────────────────────────────────────────────────────────

/// Primary CTA button — solid hot-pink, uppercase, monospaced.
let cyberButton (label: string) =
    Html.buttonc
        "relative px-6 py-2 bg-cyber-pink text-black font-mono font-bold uppercase tracking-widest text-sm \
         border border-cyber-pink \
         hover:bg-transparent hover:text-cyber-pink hover:shadow-[0_0_12px_#ff2d78] \
         active:scale-95 transition-all duration-150 animate-pulse-pink"
        [ text label ]

/// Ghost / outline button variant.
let cyberButtonGhost (label: string) =
    Html.buttonc
        "px-6 py-2 bg-transparent text-cyber-pink font-mono font-bold uppercase tracking-widest text-sm \
         border border-cyber-pink \
         hover:bg-cyber-pink/10 hover:shadow-[0_0_8px_#ff2d78] \
         active:scale-95 transition-all duration-150"
        [ text label ]

/// Danger / destructive button.
let cyberButtonDanger (label: string) =
    Html.buttonc
        "px-6 py-2 bg-transparent text-white font-mono font-bold uppercase tracking-widest text-sm \
         border border-white/30 \
         hover:border-cyber-pink hover:text-cyber-pink \
         active:scale-95 transition-all duration-150"
        [ text label ]

/// Monospaced heading with neon glow.
let cyberHeading (level: int) (label: string) =
    let sizeClass =
        match level with
        | 1 -> "text-4xl"
        | 2 -> "text-2xl"
        | 3 -> "text-xl"
        | _ -> "text-base"
    Html.divc
        $"font-mono font-bold uppercase tracking-widest text-white {sizeClass} \
          drop-shadow-[0_0_6px_#ff2d78] animate-hud-appear"
        [
            Html.spanc "text-cyber-pink" [ text "// " ]
            text label
        ]

/// Status badge — mimics ammo / health counter readouts.
let hudBadge (label: string) (value: string) =
    Html.divc "flex flex-col items-center gap-0.5" [
        Html.spanc "font-mono text-xs text-white/40 uppercase tracking-widest" [ text label ]
        Html.spanc "font-mono text-2xl font-bold text-white tabular-nums leading-none" [ text value ]
    ]

/// Thin neon horizontal divider.
let neonDivider () =
    Html.divc "w-full h-px bg-gradient-to-r from-transparent via-cyber-pink to-transparent opacity-60" []

/// Input field: dark fill, thin pink border on focus.
let cyberInput (placeholder: string) =
    Html.inputc
        "w-full bg-black/50 border border-white/10 text-white font-mono text-sm px-3 py-2 \
         placeholder-white/25 \
         focus:outline-none focus:border-cyber-pink focus:shadow-[inset_0_0_4px_#ff2d7844] \
         transition-all duration-150"
        [ Attr.placeholder placeholder ]

/// Tag / chip component.
let cyberTag (label: string) =
    Html.spanc
        "inline-block px-2 py-0.5 border border-cyber-pink/50 text-cyber-pink font-mono text-xs uppercase tracking-wider"
        [ text label ]

/// Notification / alert card.
let hudAlert (kind: string) (msg: string) =
    let kindClass =
        match kind with
        | "warn" -> "text-yellow-400 border-yellow-400/30"
        | "ok"   -> "text-green-400 border-green-400/30"
        | _      -> "text-cyber-pink border-cyber-pink/30"
    Html.divc $"flex items-start gap-3 p-3 border bg-white/5 {kindClass}" [
        Html.spanc "font-mono text-xs font-bold uppercase tracking-widest mt-0.5 shrink-0" [ text $"[{kind}]" ]
        Html.spanc "font-mono text-xs text-white/80" [ text msg ]
    ]

/// Thin neon progress bar.
let hudProgress (pct: int) =
    Html.divc "w-full h-1.5 bg-white/10 relative" [
        Html.divc "h-full bg-cyber-pink shadow-[0_0_4px_#ff2d78]" [
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

// ── Scan-line CRT overlay ─────────────────────────────────────────────────────

let private scanLineOverlay () =
    Html.divc "pointer-events-none fixed inset-0 z-50 overflow-hidden" [
        Html.divc
            "absolute left-0 right-0 h-16 \
             bg-gradient-to-b from-transparent via-white/[0.02] to-transparent \
             animate-scan-line"
            []
        Html.divc "absolute inset-0" [
            Attr.style
                "background: repeating-linear-gradient(0deg, transparent, transparent 2px, rgba(0,0,0,0.04) 2px, rgba(0,0,0,0.04) 4px)"
        ]
    ]

// ── Glitch title ──────────────────────────────────────────────────────────────

let private glitchTitle () =
    Html.divc "relative inline-block select-none" [
        Html.spanc
            "font-mono font-black uppercase text-5xl text-white tracking-[0.2em] \
             drop-shadow-[0_0_12px_#ff2d78] animate-flicker block"
            [ text "V2 THEME" ]
        Html.spanc
            "absolute inset-0 font-mono font-black uppercase text-5xl text-cyber-pink \
             tracking-[0.2em] opacity-70 animate-glitch block pointer-events-none"
            [ Attr.style "clip-path: inset(15% 0 75% 0)"; text "V2 THEME" ]
        Html.spanc
            "absolute inset-0 font-mono font-black uppercase text-5xl text-cyan-400 \
             tracking-[0.2em] opacity-40 animate-glitch-clip block pointer-events-none"
            [ Attr.style "clip-path: inset(65% 0 10% 0); transform: translate(3px)"; text "V2 THEME" ]
    ]

// ── Animation demo tile ───────────────────────────────────────────────────────

let private animTile (name: string) (animClass: string) =
    hudFrame "p-4 flex flex-col items-center gap-2 overflow-hidden" [
        Html.divc $"w-8 h-8 border-2 border-cyber-pink {animClass}" []
        Html.spanc "font-mono text-xs text-white/40 uppercase tracking-widest" [ text name ]
    ]

// ── Main view ─────────────────────────────────────────────────────────────────

let view () =
    Html.divc "min-h-screen bg-black text-white font-mono overflow-y-auto animate-power-on" [
        scanLineOverlay ()
        Html.a [
            Attr.addClass
                "fixed top-4 right-4 z-[60] inline-flex items-center justify-center border border-cyber-pink bg-black/70 px-4 py-2 text-xs font-bold uppercase tracking-widest text-cyber-pink shadow-[0_0_12px_#ff2d7833] backdrop-blur-sm transition hover:bg-cyber-pink/10 lg:hidden"
            Attr.href "#/"
            text "Exit preview"
        ]

        Html.divc "max-w-4xl mx-auto px-6 py-12 flex flex-col gap-12" [

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
                    for t in [ "STEALTH"; "HOSTILE"; "TRACKED"; "ANOMALY"; "LINKED" ] do
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
                                Html.spanc "text-white/40 text-xs" [ text "HEALTH" ]
                                Html.spanc "text-white text-xs" [ text "87%" ]
                            ]
                            hudProgress 87
                            Html.divc "flex justify-between mt-1" [
                                Html.spanc "text-white/40 text-xs" [ text "ARMOR" ]
                                Html.spanc "text-white text-xs" [ text "52%" ]
                            ]
                            hudProgress 52
                            Html.divc "flex justify-between mt-1" [
                                Html.spanc "text-white/40 text-xs" [ text "SIGNAL" ]
                                Html.spanc "text-white text-xs" [ text "100%" ]
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
            // The scan-line animation translates from -100% to 100vh, so we show
            // a sweeping line element rather than putting it on the preview box.
            section "ANIMATIONS" [
                Html.divc "grid grid-cols-2 md:grid-cols-4 gap-4 text-center" [
                    animTile "GLITCH"    "animate-glitch"
                    animTile "FLICKER"   "animate-flicker"
                    animTile "HUD IN"    "animate-hud-appear"
                    animTile "POWER ON"  "animate-power-on"
                    animTile "PULSE"     "animate-pulse-pink"
                    animTile "SLIDE DN"  "animate-slide-down"
                    animTile "GLITCH 2"  "animate-glitch-clip"
                    hudFrame "p-4 flex flex-col items-center gap-2 overflow-hidden" [
                        Html.divc
                            "w-full h-0.5 bg-cyber-pink opacity-70 animate-scan-line"
                            []
                        Html.spanc "font-mono text-xs text-white/40 uppercase tracking-widest" [
                            text "SCAN LINE"
                        ]
                    ]
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
