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
    HoursSort: int
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
        HoursSort = 0
        Categories = [
            "Fun"
        ]
    }

type private ItineraryItem = { Time: string; Event: string }

module private ItineraryItem =
    let view item =
        fragment [
            Html.divc "pl-2 font-bold border-r border-thistle-50" [
                Html.spanc "font-bold" [
                    text item.Time
                ]
            ]
            Html.spanc "col-span-2" [
                text item.Event
            ]
        ]

module private Itinerary =
    let build = List.map ItineraryItem.view >> Html.divc "grid grid-cols-3 gap-x-7"

let private locations = [
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
            HoursSort = 10
            Categories = [
                "Museum"
                "Archaeology"
                "Island"
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
            Hours = "\"Only open during summer exhibitions\""
            HoursSort = 9999
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
            HoursSort = 900
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
            HoursSort = 100
            Categories = [
                "Art"
            ]
    }
    {
        Location.empty with
            Title = "Marché provençal Forville"
            Desc =
                "Cannes' large covered market, Marché Forville, located near its famous La Croisette boardwalk and at the foot of the ancient neighbourhood of Le Suquet, is a local institution."
            SiteLink =
                "https://www.cannes.com/fr/mairie/annuaire-pratique/equipements-municipaux/marche-provencal-forville.html"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/March%C3%A9+Forville,+Rue+du+March%C3%A9+Forville,+Cannes,+France/@43.551139,7.0130888,18z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce818bff953b87:0xc34461ffc2abf361!2m2!1d7.0120273!2d43.5520794?entry=ttu"
            Dfp = "600m ~7min"
            Hours = "7:30am - 12:30pm"
            HoursSort = 1
            Categories = [
                "Market"
            ]
    }
    {
        Location.empty with
            Title = "Les Murs Peints"
            Desc = "Painted walls of France"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Les+Murs+Peints+-+Cannes+movie+car+museum,+Cannes,+France/@43.5522401,7.0173756,16z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce81eb573b1d15:0x71b5d79f22253188!2m2!1d7.023246!2d43.5566686?entry=ttu"
            Dfp = "2.5km ~16min"
            Cost = "free"
            Tour = "no"
            HoursSort = 800
            Categories = [
                "Art"
                "Architecture"
            ]
    }
    {
        Location.empty with
            Title = "Musée des explorations du monde"
            Desc =
                "Located above the old quarter of Cannes, the Musée des explorations du monde (formerly Musée de la Castre) opens on prestigious collections of primitive art, Mediterranean antiques, musical instruments from around the world and 19th century landscape paintings."
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Mus%C3%A9e+des+explorations+du+monde,+Rue+de+la+Castre,+Cannes,+France/@43.5509425,7.0122779,18z/data=!3m1!4b1!4m13!4m12!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce811c78106eb5:0x6895f8622ddf4934!2m2!1d7.0104453!2d43.5498679?entry=ttu"
            Dfp = "800m ~12min"
            Hours = "10am - 1pm, 2pm - 5pm"
            HoursSort = 5
            Categories = [
                "Art"
            ]
    }
    {
        Location.empty with
            Title = "Rue d'Antibes"
            Desc =
                "Rue d'Antibes in Cannes is the city's major shopping district and offers a wide range of upscale, boutique shopping experiences with designer clothes and big-name designers. Here you can find elegant antique furniture, fantastic jewelry and an array of upscale shops along this street."
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Rue+d'Antibes,+06400+Cannes,+France/@43.5512832,7.01629,17z/data=!3m1!4b1!4m14!4m13!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce8194079bc03d:0x644133ce48199d6d!2m2!1d7.023147!2d43.5524546!3e2?entry=ttu"
            Dfp = "800m ~10min"
            Cost = "free (to look)"
            Tour = "no"
            HoursSort = 700
            Categories = [
                "Shopping"
                "Clothing"
                "Jewelry"
            ]
    }
    {
        Location.empty with
            Title = "Palais Des Festivals et des congres"
            Desc =
                "The Palais des Festivals et des Congrès (Palace of Festivals and Conferences) is a convention centre in Cannes, France. It is the primary venue for the annual Cannes Film Festival, the Cannes Lions International Festival of Creativity and the NRJ Music Awards. The building, as known today, was unveiled in 1982."
            SiteLink = "https://en.palaisdesfestivals.com/"
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Palais+des+Festivals+et+des+Congr%C3%A8s+de+Cannes,+Boulevard+de+la+Croisette,+Cannes,+France/@43.5507405,7.015094,18z/data=!3m2!4b1!5s0x47f378c080f54657:0xd8c4e84bcfe8415d!4m14!4m13!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce8191f03733bb:0xafb954664bccc079!2m2!1d7.0179622!2d43.5507813!3e2?entry=ttu"
            Dfp = "260m ~3min"
            Cost = "free"
            Tour = "no"
            HoursSort = 500
            Categories = [
                "Convention Centre"
                "Film"
                "Likely Not Touristy"
            ]
    }
    {
        Location.empty with
            Title = "La Croix des Gardes"
            Desc =
                "A dream setting immortalized in the cinema, a breathtaking view, a Florentine-inspired architecture… Since the beginning of the 20th century, the Château de la Croix Des Gardes has been part of the mythical properties of the Côte D’Azur. Today, the chateau is the only remaining castle to overlook the Bay of Cannes from sunrise to sunset; with breathtaking 360°-degree views over the Lérins Islands, the Mediterranean Sea, and the snow-capped mountains."
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/La+Croix+des+Gardes,+Cannes,+France/@43.5527048,6.9918527,15z/data=!3m2!4b1!5s0x47f378c080f54657:0xd8c4e84bcfe8415d!4m14!4m13!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce823e09193167:0x377b54bbe143a49e!2m2!1d6.9880335!2d43.5559449!3e2?entry=ttu"
            Dfp = "3.2km ~46min"
            Cost = "free"
            Tour = "no"
            HoursSort = 600
            Categories = [
                "Garden"
                "Chateau"
                "Park"
                "Likely Not Touristy"
            ]
    }
    {
        Location.empty with
            Title = "Abbaye de Lerins/Iles de Lerins"
            Desc =
                "Lérins Abbey is a Cistercian monastery on the island of Saint-Honorat, one of the Lérins Islands, on the French Riviera, with an active monastic community. "
            DfpLink =
                "https://www.google.com/maps/dir/Vieux+Port+de+Cannes,+Jet%C3%A9e+Albert+Edouard,+Cannes,+France/Abbaye+de+L%C3%A9rins,+%C3%8Ele+Saint-Honorat,+Cannes,+France/@43.5290109,7.0092338,14z/data=!3m2!4b1!5s0x47f378c080f54657:0xd8c4e84bcfe8415d!4m14!4m13!1m5!1m1!1s0x12ce818ee1928f9f:0xeebd2f37ab1ffd22!2m2!1d7.0145829!2d43.5500061!1m5!1m1!1s0x12ce81287e6569d5:0x94dc4d15a5fbe815!2m2!1d7.0472839!2d43.5065235!3e2?entry=ttu"
            Dfp = "6.6km ~34min"
            Cost = "free"
            Tour = "no"
            Hours = """ "Open to visitors during the day" """
            HoursSort = 8999
            SiteLink = "https://abbayedelerins.com/site/fr/"
            Categories = [
                "Island"
                "Monastery"
            ]
    }
    {
        Location.empty with
            Title = "Rue Meynadier"
            Desc = "Historic street lined by old buildings"
            HoursSort = 10_000
            Categories = [
                "Historic"
                "Street"
                "Minimal Research"
            ]
    }
    {
        Location.empty with
            Title = "Ile Sainte-Marguerite"
            Desc = "The museum shows archaeological objects recovered and contents of a shipwreck"
            HoursSort = 10_001
            Categories = [
                "Museum"
                "Minimal Research"
            ]
    }
    {
        Location.empty with
            Title = "Le Monastere Fortifie"
            Desc = "Ancient Cistercian stronghold"
            HoursSort = 10_002
            Categories = [
                "Landmark"
                "Minimal Research"
            ]
    }
    {
        Location.empty with
            Title = "Eglise Notre Dame d'Esperance"
            Desc = "Build built between 1521 and 1627 and is the oldest church in Cannes."
            HoursSort = 10_003
            Categories = [
                "Church"
                "Cathedral"
                "Minimal Research"
            ]
    }
]

let private mustSee = [
    "Promenade de la Croisette"
    "Musée des explorations du monde"
    "Suquet Des Artistes"
    "Marché provençal Forville"
    "Rue d'Antibes"
]

let private mustSeeLocations =
    locations
    |> List.filter (fun l -> mustSee |> List.contains l.Title)
    |> List.sortBy (fun l -> mustSee |> List.findIndex (fun m -> l.Title = m))

let private niceLocations =
    (Set.ofList locations, Set.ofList mustSeeLocations)
    |> fun (l, m) -> Set.difference l m
    |> Set.toList
    |> List.sortBy (fun l -> l.Title)

let private itinerary = [
    {
        Time = "9:00am"
        Event = "Depart Ship"
    }
    {
        Time = "10:00am"
        Event = "Marché provençal Forville"
    }
    { Time = "11:00am"; Event = "Open" }
    { Time = "12:00am"; Event = "Open" }
    {
        Time = "1:00am"
        Event = "Suquet Des Artistes"
    }
    {
        Time = "2:00am"
        Event = "Promenade de la Croisette"
    }
    { Time = "3:00am"; Event = "Open" }
    {
        Time = "4:00am"
        Event = "Musée du Masque"
    }
    { Time = "5:00am"; Event = "Open" }
    { Time = "6:00am"; Event = "Open" }
    {
        Time = "7:00am"
        Event = "Board Ship"
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
                Html.small "*Loosely ranked"
                Html.ulc "" (mustSeeLocations |> List.map (location >> toListItem))
            ]
            Html.sectionc "flex flex-col lg:col-span-4" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Would be really nice to see"
                ]
                Html.small "*Unranked"
                Html.ulc "" (niceLocations |> List.map (location >> toListItem))
            ]
            Html.sectionc "" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Hours"
                ]
                Html.divc
                    "grid grid-cols-2"
                    (locations
                     |> List.sortBy (fun l -> l.HoursSort)
                     |> List.map (fun l ->
                         fragment [
                             Html.spanc "overflow-hidden whitespace-nowrap overflow-ellipsis font-bold" [
                                 text l.Hours
                             ]
                             Html.spanc "overflow-hidden whitespace-nowrap overflow-ellipsis" [
                                 text l.Title
                             ]
                         ]))
            ]
            Html.sectionc "" [
                Html.h2c "font-extrabold text-4xl" [
                    text "Potential Itinerary"
                ]
                Itinerary.build itinerary
            ]
        ]
    ]
