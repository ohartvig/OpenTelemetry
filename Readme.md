# Hva er  [OpenTeleMetry](https://opentelemetry.io/)?

- Open => Open Source
- Tele => Fjern
- Metry = > Måling

## Hvorfor 

### Vi har:
- moderne, distribuerte og komplekse systemer, bygget på NET platform

### Vi ønsker:(i denne software konteks)
- å muliggjøre effektiv overvåkning, diagnostikk og  optimalisering av applikasjoner og systemer 



### Hvordan?
 Gjennom Instrumentering og Telemetri kan vi lage applikasjoner/system med Observabitity

 Dette finnes et stort antall openSource ( [Prometheus](https://prometheus.io/),  [Jaeger](https://www.jaegertracing.io/)  )     og komersielle produkter (APM) , som bruker et systems observerbarhet , til overvåking og analyse/visualisering/etc.
 

## Hva er Observabitity (eller observerbarhet)
_Observability is generally considered to be a practice rather than a telemetry style_ 

- Evnen til å forstå,måle og få innsikt i indre funksjoner i et system ved å observere dets ekstren utdata..
- Inkludert innsamling og analyse av data,logger og metrikker , for å få et helhetlig bilde av systemets  ytelse,helse og atferd.
- Logger,Metrikker,Sporing,Varsler,Diagnostikk,Visualisering.
- Gir dypere innsikt i hvordan et system fungerer i sanntid.
- Skal ikke på virke et systems ytelse (vesentlig) .


## Hva er Telemetri:

_Telemetry is data that production systems emit to provide feedback about what is happening inside_

Automatiserte målinger og overføring avdata fra  (fjerntliggende-)kilder til mottaksutstyr for overvåking eller analyse.

_Four styles of telemetry:_<br>
_Centralized logging brings logging data generated by production systems to a central place where people can query it._<br>
_Metrics-style telemetry is about using numbers (counters, timers, rates, and the like) to get feedback about what’s going on in your production systems._ <br>
_Distributed tracing is a strange union of metrics and logging—the cardinality of logging with the analytical power of metrics._<br>
_Security Information Event Management (SIEM)—A specialized telemetry system for use by Security and Compliance teams, and a specialization of centralized logging and metrics_<br>




## Hva er Instrumentering? (i software konteks)
legge til kode eller verktøy i et program eller system for å å samle data om ytelse,atferd eller bruk..
Bidrar til observerbarhet


## Hva er OpenTelemetry:
 Cloud Native Computing Foundation (CNCF) project

En open-source observasjonsramme som skal standardisere innsamling av distribuerte sport og metrikker fra applikasjoner/systemer.
Et felles rammeverk for å legge til og innhente observasjonsdata, uavhengig av miljø/språk


#### Før OpenTeleMetry:
Ingen standard, mange verktøy med egen protokoller,agenter,klienter osv...
Vanskelig å lage (klient-)bibliotek ..

#### OpenTeleMetry:
- Standardlisert  overførings-protokoll (otlp)
- Standard for Log, Trace,Metric  (hva er dette)
- Uavhengig av platform/språk
- Støttet av de fleste leverandører av (APM)Verktøy (back-end).
- Ferdig bibliotek(nuget-pakker) for 'automatisk' instrumentering av NET komponenter som ASPNET, HTTP, SQLClient(sql-server)
- (community for utvikling av andre pakker)
 

#### Hvordan bruke OpenTelemetry i NET applikasjoner/systemer?
- Legger til instrumentering (automatiske og manuelle) til koden for å samle inn Spor(trace) og metrikker
- Bruker ferdig-pakker for å sende innsamlete data direkete til back-end , (eller til en Collector (Hub))
- Støtte for Log, Trace(distibuert) og Metrics..
 - [NET observability with OpenTelemetry](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel)

OpenTeleMetry er Microsoft sin default instrumentering av componenter for NET6+ 

Innebygget støtte i System.Diagnostic  (Activity)

### Hva er Trace (Spor) (i kontekts av distibuerte systemer)?
en sekvens av hendelser og interaksjon i løpet av en førespørsel, eller transaksjon gjennom ulike komponenter.

Trace hjelper til å identifisere flaskehalser, feil og optimaliseringsmuligheter

I OpenTeleMetry brukes  (hierarkisk )Span  (NET: Activity) 

### Hva er metrikker (i kontekts av distribuerte systemer)?
- kvantitative målinger som gir informasjon om ytelse, tilstand og atferd til system
- tall (Gauge), tellinger (Counter), forholdstall (Histogram)  



# Oppgaver :

Start med Metric -branch

Test at Metric branch fungerer: (start docker med:   'docker  compose up -d' i terminal vindu) 

1)  Opprett et nytt back webapi-prosjekt 'mine'
2)   kall 'mine'  fra Front
3)   Sjekk Trace i Jaeger...
4)   Legg til OpenTeleMetry i 'mine' med støtte for Trace
      Gjentatt 2 og 3...  forskjell?
5)  Utvid med flere 'aktiviteter' (Span)   (f.eks http-kall, ef, eller intern tidskrevende kode)   .. og gjenta 2,3
6)  Opprett noe metrics/målinger  ( Counter, Gauge)
7)  Konfiguerer Prometheus til å hente metric fra 'mine'...og sjekk at disse blir synelige i Prometheus
8)  Dagens Julenøtt: Legg til Metrics for "Microsoft.AspNetCore.Hosting" , og hent i Prometheus

9) Bonus: Grafana
 - Start Grafana (som docker -container (utvide docker-compose ))
-  Koble til Prometheus
-  Lag et enkelt dashbord med noen av målingene fra 6)

10) VIP
    - Lag en pakke med alle nødvendig OpenTeleMentry kall, som kan inluderes i all web-prosjekt
    -  (som Fhi.HelseId)

