module Cannes

open Sutil
open Sutil.CoreElements

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
    Cost: string
    Tour: string
    Hours: string
    Categories: string list
}

module private Location =
    let empty = {
        Title = "Quelque chose de cool en français"
        Desc = "Lorem ipsum"
        Dfp = "~"
        DfpLink = ""
        SiteLink = ""
        Cost = "?"
        Tour = "?"
        Hours = "?"
        Categories = []
    }

let private niceLocations = [
    {
        Location.empty with
            Title = "Musée du Masque de fer et du Fort Royal"
            Desc =
                "Tucked away in the Fort Royal on Île Sainte-Marguerite in the Bay of Cannes, \
             the Musée du Masque de Fer et du Fort Royal (formerly the Musée de la Mer) is \
             an archaeology museum exploring land and sea."
            SiteLink = "https://www.cannes.com/en/museums-arts/musee-du-masque-de-fer-et-du-fort-royal.html"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/%C3%8Ele+Sainte-Marguerite,+Cannes,+France/@43.535578,7.0097042,14z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce811e25b43827:0x1118422cc67f8a6f!2m2!1d7.0484999!2d43.5199254?entry=ttu"
            Dfp = "5.5km ~37min"
            Hours = "10am - 5:45pm"
            Categories = [
                "Museum"
                "Archaeology"
            ]
    }
    {
        Location.empty with
            Title = "Villa Domergue"
            Desc =
                "Formerly Villa Fiesole. The villa and grounds are very much influenced by the Italian style \
             and were built in 1934 on land Jean-Gabriel Domergue bought at the end of Avenue de la Californie."
            SiteLink = "https://www.cannes.com/en/museums-arts/villa-domergue.html"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Villa+Domergue,+Avenue+Fiesole,+Cannes,+France/@43.5545114,7.0280037,16z/data=!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce81c60298e81b:0xab73c5db45e6a5fd!2m2!1d7.0412728!2d43.5598969?entry=ttu"
            Dfp = "3.7km ~10min"
            Cost = "free"
            Tour = "yes"
            Categories = [
                "Museum"
                "Art Gallery"
                "Garden"
                "Tour"
            ]
    }
    {
        Location.empty with
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
            Cost = "free"
            Categories = [
                "Film"
                "Beach"
                "Shopping"
                "Hotels"
            ]
    }
    {
        Location.empty with
            Title = "Suquet Des Artistes"
            Desc =
                "The artist residence has been set up by Cannes Town Hall and is a place where Cannes artists can express themselves in the heart of Le Suquet."
            SiteLink = "https://www.cannes.com/en/museums-arts/suquet-des-artistes.html"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Suquet+des+Art(iste)s+-+R%C3%A9sidence+d'artistes,+Rue+Saint-Dizier,+Cannes,+France/@43.551246,7.0101157,17z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce818997a4f2cd:0x703a4f6636be6241!2m2!1d7.00875!2d43.5509721?entry=ttu"
            Dfp = "900m ~12min"
            Hours = "1pm - 5pm"
            Categories = [
                "Art"
            ]
    }
    {
        Location.empty with
            Title = "Marché provençal Forville"
            Desc = "Cannes' large covered market, Marché Forville, located near its famous La Croisette boardwalk and at the foot of the ancient neighbourhood of Le Suquet, is a local institution."
            SiteLink = "https://www.cannes.com/fr/mairie/annuaire-pratique/equipements-municipaux/marche-provencal-forville.html"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/March%C3%A9+Forville,+Rue+du+March%C3%A9+Forville,+Cannes,+France/@43.551139,7.0130888,18z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce818bff953b87:0xc34461ffc2abf361!2m2!1d7.0120273!2d43.5520794?entry=ttu"
            Dfp = "600m ~7min"
            Hours = "7:30am - 12:30pm"
            Categories = [
                "Market"
            ]
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
            Html.divc
                "flex flex-row flex-wrap gap-2 p-2"
                (l.Categories
                 |> List.map (fun c ->
                     Html.divc "rounded-full px-2 bg-thistle-50 text-eerie-black" [
                         text c
                     ]))
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
            Html.div [
                Html.span "Hours: "
                Html.span l.Hours
            ]
            Html.div [
                Html.span "Cost: "
                Html.span l.Cost
            ]
            Html.div [
                Html.span "Guided tour: "
                Html.span l.Tour
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
        Html.divc "mb-52 grid grid-cols-1 lg:grid-cols-6 gap-14 lg:gap-10" [
            Html.sectionc "lg:col-span-4" [
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
            Html.sectionc "lg:col-span-2" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Must See"
                ]
                Html.pc "" [
                    text "At least one thing, I'm sure"
                ]
            ]
            Html.sectionc "flex flex-col gap-3 lg:col-span-4" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Would be really nice to see"
                ]
                Html.ulc "" (niceLocations |> List.map (location >> toListItem))
            ]
            Html.sectionc "" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Potential Itinerary"
                ]
                Html.divc "grid grid-cols-3 gap-x-7" [
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "9:00am"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Depart ship"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "10:00am"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "11:00am"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "12:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "1:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "2:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "3:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "4:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "5:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "6:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Something fun"
                    ]
                    Html.divc "pl-2 font-bold border-r border-thistle-50" [
                        Html.spanc "font-bold" [
                            text "7:00pm"
                        ]
                    ]
                    Html.spanc "col-span-2" [
                        text "Board ship"
                    ]
                ]
            ]
        ]
    ]
