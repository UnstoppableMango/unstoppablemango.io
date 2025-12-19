module Wishlist

open Sutil

type Item = {
    Name: string
    Link: string option
    Image: string option
    Description: string option
}

let items = [
    {
        Name = "Geometric wall light thing"
        Link = Some "https://www.amazon.com/led-wall-panels/s?k=led+wall+panels"
        Image = None
        Description = Some "Something like the stuff in the link"
    }
    {
        Name = "Under desk RGB lights"
        Link = None
        Image = None
        Description = Some "Would be nice to have management software and bluetooth or USB. Standalone is fine though"
    }
    {
        Name = "Surround sound system"
        Link = None
        Image = None
        Description = Some "Probably ignore this one... it's something I want though :)"
    }
    {
        Name = "Candles"
        Link = None
        Image = None
        Description = Some "Need more smelly stuff for the house. Not very picky, just no sandalwood please"
    }
    {
        Name = "Settle Your Scores, Better Luck Tomorrow Vinyl"
        Link =
            Some
                "https://www.merchbar.com/rock-alternative/settle-your-scores/settle-your-scores-better-luck-tomorrow-cd"
        Image = None
        Description =
            Some
                "I don't think this exists at the moment, but if you somehow manage to find it that would make my day. Link is for the CD. Would also like The Wilderness vinyl by the same artist"
    }
    // {
    //     Name = "Chainsaw man manga"
    //     Link = Some "https://www.booksamillion.com/search?filter=&id=8929272676791&query=chainsaw+man"
    //     Image = None
    //     Description = None
    // }
    {
        Name = "Isaac Asimov Books"
        Link = Some "https://www.goodreads.com/search?utf8=%E2%9C%93&query=isaac+asimov"
        Image = None
        Description = Some "Looking to start a collection. I, Robot would be a good first book"
    }
    {
        Name = "Ember travel mug"
        Link = Some "https://ember.com/products/ember-travel-mug-2"
        Image = None
        Description = None
    }
    {
        Name = "More bed sheets"
        Link = None
        Image = None
        Description =
            Some
                "I currently have gray, but I could get an entirely new color. Just needs to go with my bed frame. I believe I like the higher thread counts."
    }
    {
        Name = "Tongue scraper"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Top Gear UK"
        Link = None
        Image = None
        Description =
            Some "Doesn't need to start with Season 1, any season with Clarkson, Hammond, and May. Or Grand Tour"
    }
    {
        Name = "Slippers"
        Link = None
        Image = None
        Description = Some "Current ones are smelly. This might be an annual gift"
    }
    {
        Name = "Spirit Island board game"
        Link = Some "https://greaterthangames.com/product/spirit-island/"
        Image = None
        Description = None
    }
    {
        Name = "Domain Modeling Made Functional by Scott Wlaschin"
        Link = Some "https://pragprog.com/titles/swdddf/domain-modeling-made-functional/"
        Image = None
        Description = None
    }
    {
        Name = "The Road Not Taken - Harry Turtledove"
        Link = None
        Image = None
        Description =
            Some
                "Looking at this again I think it was just a short story published in a SciFi magazine. Would be cool if there was a book with a compilation of Harry's short stories"
    }
    {
        Name = "Kill it with fire book"
        Link = Some "https://www.goodreads.com/book/show/54716655-kill-it-with-fire"
        Image = None
        Description = None
    }
    {
        Name = "Mop and mop accessories"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "N64 and N64 accessories"
        Link = None
        Image = None
        Description =
            Some
                "I got an Analogue 3D and need both N64 games and controllers, I don't own any at all. Mario, Banjo-Kazooie, Zelda, Kirby, etc."
    }
    {
        Name = "8bitdo N64 controller"
        Link = Some "https://www.8bitdo.com/64-controller/"
        Image = None
        Description = Some "To go with the Analogue 3D"
    }
    {
        Name = "Deathstalker by Simon R. Green"
        Link =
            Some
                "https://www.goodreads.com/book/show/629068.Deathstalker?from_search=true&from_srp=true&qid=6prwSU3M7o&rank=1"
        Image = None
        Description = None
    }
    {
        Name = "Children of Time Series"
        Link = Some "https://www.goodreads.com/series/247630-children-of-time"
        Image = None
        Description = None
    }
    {
        Name = "Master Transmuter card/poster/wall art"
        Link = Some "https://gatherer.wizards.com/Pages/Card/Details.aspx?multiverseid=179252"
        Image = None
        Description = Some "I'd like any kind of decoration that depicts this card or the picture on the card"
    }
]

let loggedOut () =
    Html.divc "h-screen bg-eerie-black/50 flex items-center lg:justify-center px-2" [
        Html.divc "lg:w-1/3 flex flex-col lg:gap-2" [
            Html.h1c "text-thistle-50 font-extrabold text-5xl" [
                text "Spend your money on something useful"
            ]
            Html.a [
                Attr.addClass "text-thistle-50 text-xs"
                Attr.href "https://donate.wikimedia.org"
                text "https://donate.wikimedia.org"
            ]
        ]
    ]

let wishlist () =
    Html.divc "h-screen p-2 xl:w-1/2 xl:mx-auto flex flex-col gap-3 bg-white/60" [
        Html.h1c "text-thistle-50 font-extrabold text-5xl" [
            text "Some things..."
        ]
        Html.pc "text-thistle-50" [
            text "NOT ordered by priority"
        ]
        Html.ulc "xl:w-[90%] xl:mx-auto overflow-y-scroll" [
            for item in items do
                Html.lic "bg-thistle-50/90 mb-3 px-2 py-1 rounded-lg drop-shadow-lg" [
                    Html.divc "flex flex-row justify-between" [
                        Html.h4c "text-lg font-bold" [
                            text item.Name
                        ]
                        match item.Link with
                        | Some link ->
                            Html.ac "flex flex-row items-center gap-1 text-sm" [
                                Attr.href link
                                Attr.targetBlank
                                Html.p "This thing"
                                Html.ic "fa-solid fa-arrow-up-right-from-square" []
                            ]
                        | _ -> Html.none
                    ]
                    match item.Description with
                    | Some d ->
                        Html.pc "" [
                            text d
                        ]
                    | _ -> Html.none
                ]
        ]
    ]

let view () = wishlist ()
