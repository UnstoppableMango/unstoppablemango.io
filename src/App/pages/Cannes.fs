module Cannes

open Sutil

let toListItem e =
    Html.lic "mb-2" [
        e
    ]

type private Location = {
    Title: string
    Desc: string
    Dfp: string
    DfpLink: string
    SiteLink: string
}

let private niceLocations = [
    {
        Title = "Musée du Masque de fer et du Fort Royal"
        Desc =
            "Tucked away in the Fort Royal on Île Sainte-Marguerite in the Bay of Cannes, \
             the Musée du Masque de Fer et du Fort Royal (formerly the Musée de la Mer) is \
             an archaeology museum exploring land and sea."
        SiteLink = "https://www.cannes.com/en/museums-arts/musee-du-masque-de-fer-et-du-fort-royal.html"
        DfpLink =
            "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/%C3%8Ele+Sainte-Marguerite,+Cannes,+France/@43.535578,7.0097042,14z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce811e25b43827:0x1118422cc67f8a6f!2m2!1d7.0484999!2d43.5199254?entry=ttu"
        Dfp = "5.5km ~37min"
    }
    {
        Title = "Villa Domergue"
        Desc =
            "Formerly Villa Fiesole. The villa and grounds are very much influenced by the Italian style \
             and were built in 1934 on land Jean-Gabriel Domergue bought at the end of Avenue de la Californie."
        SiteLink = "https://www.cannes.com/en/museums-arts/villa-domergue.html"
        DfpLink =
            "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Villa+Domergue,+Avenue+Fiesole,+Cannes,+France/@43.5545114,7.0280037,16z/data=!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce81c60298e81b:0xab73c5db45e6a5fd!2m2!1d7.0412728!2d43.5598969?entry=ttu"
        Dfp = "3.7km ~10min"
    }
    {
        Title = "Promenade de la Croisette"
        Desc =
            "The Promenade de la Croisette, or Boulevard de la Croisette, is a prominent road in Cannes, France. \
             It stretches along the shore of the Mediterranean Sea and is about 2 km long. The Croisette is known \
             for the Palais des Festivals et des Congrès, where the Cannes Film Festival is held. Many expensive shops, \
             restaurants, and hotels (such as the Carlton, Majestic, JW Marriott Cannes, and Martinez) line the road. \
             It goes completely along the coastline of Cannes."
        SiteLink = "https://en.wikipedia.org/wiki/Promenade_de_la_Croisette"
        DfpLink =
            "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Promenade+de+la+Croisette,+Boulevard+de+la+Croisette,+Cannes,+France/@43.5506722,7.017028,17z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce81965aa9c7dd:0xe7d1325b729e3b12!2m2!1d7.0246028!2d43.5498695?entry=ttu"
        Dfp = "800m ~3min"
    }
]

let private location l =
    Html.details [
        Attr.addClass "group"
        Html.summary [
            Attr.addClass "cursor-pointer text-xl font-bold"
            text l.Title
        ]
        Html.divc "animate-slide-down" [
            Html.pc "m-1 px-2 pb-1 rounded-md text-gray-100 bg-white/10" [
                text l.Desc
            ]
            Html.div [
                Html.span "Distance from port: "
                Html.ac "underline" [
                    Attr.href l.DfpLink
                    Attr.targetBlank
                    text l.Dfp
                ]
            ]
            Html.ac "underline" [
                Attr.href l.SiteLink
                Attr.targetBlank
                text "Website"
            ]
        ]
    ]

let view () =
    Html.divc "h-screen overflow-y-scroll px-1 lg:p-10 bg-eerie-black/75 text-thistle-50" [
        Html.divc "mb-52 flex flex-col gap-14" [
            Html.sectionc "w-full" [
                Html.h2c "font-extrabold text-5xl" [
                    text "Cannes"
                ]
                Html.pc "m-1 px-2 pb-1 rounded-md text-gray-100 bg-white/10" [
                    text "Cannes "
                    Html.ic "text-xs" [
                        text "(/kæn, kɑːn/ KAN, KAHN, French: [kan], locally [ˈkanə]; Occitan: Canas) "
                    ]
                    text
                        "is a city located on the French Riviera. It is a commune located in the Alpes-Maritimes \
                        department, and host city of the annual Cannes Film Festival, Midem, and Cannes Lions International \
                        Festival of Creativity. The city is known for its association with the rich and famous, \
                        its luxury hotels and restaurants, and for several conferences."
                ]
                Html.divc "ml-5" [
                    Html.span "- "
                    Html.ac "underline" [
                        Attr.href "https://en.wikipedia.org/wiki/Cannes"
                        Attr.targetBlank
                        text "Wikipedia"
                    ]
                ]
                Html.divc "text-center mt-5" [
                    Html.ac "underline" [
                        Attr.href "https://www.cannes.com/en/index.html"
                        Attr.targetBlank
                        text "Official Website"
                    ]
                ]
            ]
            Html.sectionc "" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Must See"
                ]
                Html.pc "" [
                    text "At least one thing, I'm sure"
                ]
            ]
            Html.sectionc "flex flex-col gap-3" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Would be really nice to see"
                ]
                Html.ulc "" (niceLocations |> List.map (location >> toListItem))
            ]
        ]
    ]
