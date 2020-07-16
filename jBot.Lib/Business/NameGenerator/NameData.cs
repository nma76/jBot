using System;
using System.Collections.Generic;
using jBot.Lib.Models;

namespace jBot.Lib.Business.NameGenerator
{
    public static class NameData
    {
        public readonly static List<Person> Referees = new List<Person>()
        {
            new Person()
            {
                Name = "Johan Nordlöf",
                Type = "Huvuddomare",
                Description = "Årets domare i Hockeyallsvenskan 2019, och således och vinnare av Guldtröjan."
            },
            new Person()
            {
                Name = "Christoffer Holm",
                Type = "Huvuddomare",
                Description = "Yngsta domaren i SHL just nu, född 1992. Har dömt som huvuddomare i HockeyAllsvenskan sedan säsongen 2015/2016."
            },
            new Person()
            {
                Name = "Richard Magnusson",
                Type = "Huvuddomare",
                Description = "Lillebror till SHL-domaren Johan Magnusson. Har dömt som huvuddomare i HockeyAllsvenskan i fyra säsonger och dessförinnan fem säsonger i HockeyEttan."
            },
            new Person()
            {
                Name = "Johan Magnusson",
                Type = "Huvuddomare",
                Description = "Storebror till SHL-domaren Richard Magnusson. Före detta spelare i både BIK Karlskoga som Tranås som avslutade sin spelarkarriär säsongen 2012/2013."
            },
            new Person()
            {
                Name = "Mikael Andersson",
                Type = "Huvuddomare",
                Description = "Kallas för 'Micke Åkers', gillar att laga soppor och tränar även ett innebandylag."
            },
            new Person()
            {
                Name = "Patric Bjälkander",
                Type = "Huvuddomare",
                Description = "Lämnar inför säsongen sitt jobb på Trafikverket för att bli proffsdomare. Älskar att blanda gin & tonic."
            },
            new Person()
            {
                Name = "Tobias Björk",
                Type = "Huvuddomare",
                Description = "Tidigare hockeyselare som sadlade om till domare efter en skada. Främsta meriten är VM-finalen 2019"
            },
            new Person()
            {
                Name = "Pehr Claesson",
                Type = "Huvuddomare",
                Description = "Spelade högerforward som aktiv och går under smeknamnen Clabbe och Pelle."
            },
            new Person()
            {
                Name = "Wolmer Edqvist",
                Type = "Huvuddomare",
                Description = "Jobbar som HR-chef på Region Värmland. Spelade som back i unga år."
            },
            new Person()
            {
                Name = "Andreas Harnebring",
                Type = "Huvuddomare",
                Description = "Kallas för 'Bringan'. Spelar mycket golf och har 2 i handicap. Har JVM i Ostrava i största merit."
            },
            new Person()
            {
                Name = "Mikael Holm",
                Type = "Huvuddomare",
                Description = "Före detta klubbsäljare på Team Sportia och fick han välja drömyrket skulle han bli musikalartist."
            },
            new Person()
            {
                Name = "Morgan Johansson",
                Type = "Huvuddomare",
                Description = "Har dömt 752 matcher som huvuddomare i SHL.shl  Har en grabb som spelar i Trojas juniorer och en dotter som spelar i HV71. "
            },
            new Person()
            {
                Name = "Marcus Linde",
                Type = "Huvuddomare",
                Description = "Spelade hockey till han fyllde 22. Är grym på hushållssysslor och onödig kunskap."
            },
            new Person()
            {
                Name = "Mikael Nord",
                Type = "Huvuddomare",
                Description = "Meriterad domare och femfaldig vinnare av guldpipan"
            },
            new Person()
            {
                Name = "Sören Persson",
                Type = "Huvuddomare",
                Description = "Driver en padel- och squashanläggning. Annars mest känd för strypgreppet på Marius Holtet."
            },
            new Person()
            {
                Name = "Mikael Sjöqvist",
                Type = "Huvuddomare",
                Description = "Kallas 'Sjöka'. Gillar att fiska, äter helst veganskt och spelar saxofon."
            },
            new Person()
            {
                Name = "Linus Öhlund",
                Type = "Huvuddomare",
                Description = "Äter helst en blodig köttbit och kollar på Vikings. Är sjukt bra på skoter."
            }
            //,
            //new Person()
            //{
            //    Name ="Andreas Svensson",
            //    Type = "Linjedomare",
            //    Description = "Linjedomare som gjorde sin andra säsong i HockeyAllsvenskan förra säsongen. Fick säsongen 19/20 fem testmatcher i SHL."
            //},
            //new Person()
            //{
            //    Name = "Peter Almerstedt",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Fredrik Altberg",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Simon Hillborg",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Anders Jansson",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Gustav Jonsson",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Johannes Käck",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Andreas Lundén",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Ludvig Lundgren",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Johan Löfgren",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Andreas Malmqvist",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Rickard Nilsson",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Tobias Nordlander",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Anders Nyqvist",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Daniel Persson",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Henrik Pihlblad",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Jan Sandström",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Robin Werner",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Emil Wernström",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Emil Yletyinen",
            //    Type = "Linjedomare",
            //    Description = ""
            //},
            //new Person()
            //{
            //    Name = "Örjan Åhlén",
            //    Type = "Linjedomare",
            //    Description = ""
            //}
        };
    }
}
