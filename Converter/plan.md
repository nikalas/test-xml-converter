# Plan

Uppgiften är en simpel konverterare som tar en radbaserad textfil som input och konverterar till xml.

Eftersom det är en simpel demo siktar vi på en enkel implementation utan
att sväva för långt ut från spec för att hålla tiden kort, men jag försöker lägga lite energi
på en vettig pipe och struktur så det teoretiskt ska vara lätt att bygga vidare på.
Tänker inte lägga mer än några timmar på det här.

Pipen är ganska enkel: -> Input -> Parser -> Converter -> Output

Eftersom kraven alltid uttökas i verkligheten utökar jag något för en stabilare grund:

-> input -> parser -> Domänmodell -> Converter -> Output

Jag tänker att minimala interfaces för varje steg är en bra idé och ger oss möjligheten att implementera stöd för fler filformat och tvåvägs konvertering

Specen särskiljer på Person och Familj, men en generel lösning kan lika gärna använda en gemensam modell med relationsfält.

Planerat interface från cli: 
```shell
converter [-from custom][-to xml] indata.txt <utdata.xml>
```
men vi börjar med att bara köra indatafil och skriva ut xml till stdout.

jag tänker också att det kan vara en bra idé att förbereda interfaces för olika input sources (stdin, file, url) och output sinks (stdout, file) som kan implementeras i mån av tid.

Jag väljer aktivt bort services, DI och factory patterns samt tester för det här.

Oklart om det faktiskt är önskat att bygga en egen xml-writer så tänker luta mig på standard lib först men är inte mycket mer jobb att lägga till en naiv writer heller.

Om vi kollar på modell ser det ut som vi borde kunna köra typ
```
Person: 
firstName?
lastName?
birthYear?
Address? <Address>
Phone? <Phone>
relatedTo? <Person>

Address:
street
city
zip

Phone:
landline?
mobile?
```

Förmodligen klarar vi oss utan en relationstabell för familjer och kan istället använda en lista av personer med relationsfält för exempeldatan vi fått.

Det ger oss en ungefärlig struktur i stil med:

```text
Converter.sln
- Converter/
   - Program.cs

   - Domain/
      - Person.cs
      - Address.cs
      - Phone.cs

   - Input/
      - IInputSource.cs
      - FileInputSource.cs
      - StdinInputSource.cs - i mån av tid
      - UrlInputSource.cs - i mån av tid

   - Output/
      - IOutputSink.cs
      - FileOutputSink.cs
      - StdoutOutputSink.cs - i mån av tid

   - Formats/
       - IImporter.cs
       - IExporter.cs
       - Custom/
          - RecordType.cs
          - CustomImporter.cs
          - CustomExporter.cs - i mån av tid

       - Xml/
          - XmlImporter.cs - i mån av tid
          - XmlExporter.cs

       - Json/
           - JsonImporter.cs - placerholder
           - JsonExporter.cs - placerholder
```

Borde räcka som start. Får se om det håller.
