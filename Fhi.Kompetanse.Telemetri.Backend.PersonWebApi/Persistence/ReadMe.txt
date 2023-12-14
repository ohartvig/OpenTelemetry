Add-migration Init -project Fhi.Kompetanse.Telemetri.Backend.PersonWebApi -startupproject Fhi.Kompetanse.Telemetri.Backend.PersonWebApi -Context PersonContext

## fjern siste tillagt migration (kun hvis db ikke er updated)
Remove-Migration  -project Fhi.Kompetanse.Telemetri.Backend.PersonWebApi -startupproject Fhi.Kompetanse.Telemetri.Backend.PersonWebApi -Context PersonContext

## oppdater db direkte
Update-Database -project Fhi.Kompetanse.Telemetri.Backend.PersonWebApi -startupproject Fhi.Kompetanse.Telemetri.Backend.PersonWebApi -Context PersonContext