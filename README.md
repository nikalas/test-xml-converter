# Converter

## Instruktioner

Jag valde att implementera uppgiften i C#. En kortare logg över tankeprocess innan start finns under [Converter/plan.md](Converter/plan.md)

Programmet har förkompilerade binärer för de vanligaste plattformarna under [releases](https://github.com/nikalas/test-xml-converter/releases).

Programmet kan läsa från fil/stdin och skriva till fil/stdout. En exempelfil för input finns under [input.txt](input.txt)

Formaten som stöds är radbaserat enligt spec som input och xml som output, men är enkelt att utöka för tvåvägskonvertering, eller stöd för andra format som json eller csv.

Används på följande vis:

```
Description:
  Convert between data formats

Usage:
  Converter [<input> [<output>]] [options]

Arguments:
  <input>   Input file, or - for stdin
  <output>  Output file, or - for stdout

Options:
  -i, --in <in>    Input format: custom or xml
  -o, --out <out>  Output format: xml or custom
  -?, -h, --help   Show help and usage information
  --version        Show version information
```

## Integration mellan två gamla system
Det ena systemet levererar ett radbaserat filformat medan det andra kräver
XML. Du ska skriva en konverterare som bygger upp rätt XML-struktur. 
Använd valfritt programspråk!

Filformat:

```
P|förnamn|efternamn
T|mobilnummer|fastnätsnummer
A|gata|stad|postnummer
F|namn|födelseår
```

- P kan följas av T, A och F
- F kan följas av T och A
Exempel:

```
P|Carl Gustaf|Bernadotte
T|0768-101801|08-101801
A|Drottningholms slott|Stockholm|10001
F|Victoria|1977
A|Haga Slott|Stockholm|10002
F|Carl Philip|1979
T|0768-101802|08-101802
P|Barack|Obama
A|1600 Pennsylvania Avenue|Washington, D.C
```

Ger XML som:

```
<people>
<person>
<firstname>Carl Gustaf</firstname>
<lastname>Bernadotte</lastname>
<address>
<street>Drottningholms slott</street>
...
</address>
<phone>
<mobile>0768-101801</mobile>
...
</phone>
<family>
<name>Victoria</name>
<born>1977</born>
<address>...</address>
</family>
<family>...</family>
</person>
<person>...</person>
</people>
```
