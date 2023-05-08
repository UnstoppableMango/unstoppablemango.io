module Music

open Sutil
open Sutil.Core

type Artist = {
    Name: string
    SpotifyIframeSrc: string
}

let favoriteArtists = [
    {
        Name = "Electric Callboy"
        SpotifyIframeSrc = "https://open.spotify.com/embed/artist/1WNoKxsp715jez1Td4vthc?utm_source=generator"
    }
    {
        Name = "Within Destruction"
        SpotifyIframeSrc = "https://open.spotify.com/embed/artist/1kAX4yFdmR0hJe2tPu1785?utm_source=generator"
    }
    {
        Name = "The Raven Autarchy"
        SpotifyIframeSrc = "https://open.spotify.com/embed/artist/1y64eIIxdhBplQuPF7bKnQ?utm_source=generator"
    }
    {
        Name = "Settle Your Scores"
        SpotifyIframeSrc = "https://open.spotify.com/embed/artist/4QXKSmZgWNMDbQBidvuh4O?utm_source=generator"
    }
    {
        Name = "Brand of Sacrifice"
        SpotifyIframeSrc = "https://open.spotify.com/embed/artist/4d6Rawrese4OLF1zZCztod?utm_source=generator"
    }
    {
        Name = "iamjakehill"
        SpotifyIframeSrc = "https://open.spotify.com/embed/artist/26JloX1vHxGGrGUVeMItFJ?utm_source=generator"
    }
]

let allow (allowed: string seq) =
    String.concat "; " allowed |> (fun a -> Attr.custom ("allow", a))

let view () =
    Html.divc "h-screen bg-eerie-black/50 p-2 lg:p-5 flex flex-col gap-2 lg:gap-5 overflow-y-scroll" [
        Html.h1c "text-thistle-50 font-extrabold text-5xl sticky top-0" [
            text "Favorite artists"
        ]
        Html.divc "w-full grid grid-cols-1 lg:grid-cols-2 xl:grid-cols-3 gap-2 lg:gap-4" [
            for artist in favoriteArtists do
                Html.iframe [
                    Attr.addClass "w-full h-[152px] lg:h-[352px]"
                    Attr.src artist.SpotifyIframeSrc
                    allow [
                        "autoplay"
                        "fullscreen"
                        "encrypted-media"
                        "picture-in-picture"
                        "clipboard-write"
                    ]
                    Attr.custom ("loading", "lazy")
                ]
        ]
        Html.a [
            Attr.addClass
                "py-3 w-full lg:w-1/2 xl:w-1/3 rounded-full shadow backdrop-blur-2xl bg-white/70 font-bold text-eerie-black text-center uppercase self-center"
            Attr.href "#/music/artists"
            text "More"
        ]
    ]
