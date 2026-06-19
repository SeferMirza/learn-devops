# Pandoc

The aim of this project is to demonstrate how to use `Pandoc` and the `Typst` 
engine to generate `.pdf` files from `.md` files.

`Pandoc` is a universal document converter that can transform files between many 
markup formats such as `Markdown`, `HTML`, `LaTeX`, and `Word`. For more 
information, view [Pandoc](https://pandoc.org).

`Typst` is a modern alternative to `LaTeX`, designed as a typesetting system for 
creating high-quality documents with a simple syntax, built-in styling, and 
template support. For more information, view [Typst](https://typst.app).

## Setup

### Pandoc

Pandoc can be installed on Windows, macOS, and Linux using official installers 
or package managers. c 
[Pandoc Installation Guide](https://pandoc.org/installing.html)

After Pandoc installation is complete verify:

```bash
pandoc --version
```

### Typst

Typst can be installed by downloading a prebuilt binary. Download the latest 
[release](https://github.com/typst/typst/releases/tag/v0.15.0) from Github, 
extract the contents and add the `typst` executable to your system PATH.

After typst setup is complete verify:

```bash
typst --version
```

### Sample Project

This project also includes a Docker setup which allows running full pipeline 
without installing dependencies. A single `app.cs` file is run in the container, 
which starts a process that uses contens of `.\assets` folder and outputs a 
`.pdf` file to the `.\outputs` folder using `Pandoc` and `Typst`

Run following command to create a pdf file from `.\assets\input.md`:

```powershell
docker-compose up
```

## Parsing `.md` files

Pandoc parses `.md` files by first converting them into an abstract syntax tree 
(AST) structure. Markdown content is arranged into structured elements like 
headings, paragraphs, lists, links, and code blocks. After the content is 
parsed into an AST, it can be converted to other file types such as HTML, PDF, 
or Word.

Use the following command to view the native AST representation of your `.md` 
file:

```powershell
pandoc -f markdown -t native .\assets\input.md
```

## Generating `.pdf` files

`Pandoc` requires an external PDF generation engine to produce PDF files. Common 
engines include `pdfLaTeX`, `LuaLaTeX`, and `typst`. In this project, `typst` 
is used for its simplicity and easy setup.

The command below generates a PDF directly from the `input.md` file:

```powershell
pandoc .\assets\input.md -o .\output\output.pdf --pdf-engine=typst
```

It is also possible to  generate an intermediate `.typ` file and compile 
it manually:

```powershell
pandoc .\assets\input.md -t typst -o .\output\output.typ
typst compile .\output\output.typ .\output\output.pdf
```

### Filters

Pandoc includes built-in Lua filters which are tools for modifying a document’s 
structure during conversion. They can be used to modify when creating typst 
files for `.pdf` generation.

For more information, visit: https://pandoc.org/filters.html

```powershell
pandoc .\assets\input.md --lua-filter=.\assets\table-filter.lua -t native

pandoc .\assets\input.md -o .\output\output.pdf --lua-filter=.\assets\table-filter.lua --pdf-engine=typst
```

## Typst

Typst is a markup-based typesetting system used to write documents. It can also 
be used as a pdf engine for Pandoc. 

For more information, view [Typst Documentation](https://typst.app/docs/)

### Attributes

Attributes can be attached to block and inline elements. Pandoc includes these
attributes in its output for Typst to use them during document generation.

The general pattern for passing an attribute is:

```md
{typst:attribute="value"}
```

#### Block-level Attributes

```md
::: {typst:text:size="20px"}
Font size of 20
:::
```

```haskell
[ Div ("",[],[("typst:text:size","20px")])
    [ Para
        [ Str "Font"
        , Space
        , Str "size"
        , Space
        , Str "of"
        , Space
        , Str "20"
        ]
    ]
]
```

#### Inline Attributes

```md
[Confidential]{typst:text-fill="red"}
```

```haskell
Span ("",[],[("typst:text-fill","red")])
  [ Str "Confidential" ]
```

### Styling

Typst supports template files, which allow you to predefine layout, styling, and 
structure and reuse them across multiple documents. It uses `#set` rule for 
document settings and `#show` rule for modifying how elements are rendered.

```typst
#show table.cell.where(y: 0): set text(weight: "medium")

#set table(
  stroke: 0.5pt + black
)
```

For more information, view 
[Typst Style Documentation](https://typst.app/docs/reference/styling/)

Below command will use template file when generating output pdf:

```powershell
pandoc `
  .\assets\input.md `
  -o .\output\output.pdf `
  --lua-filter=.\assets\table-filter.lua `
  --pdf-engine=typst `
  --template=.\assets\template.typ
```

### Raw Typst Blocks

When generating Typst output with Pandoc, you can embed native Typst code using 
a raw Typst block.

```{=typst}
#table(
  columns: (auto, auto, auto, auto),
  align: (auto, auto, auto, auto),
  stroke: 0.5pt + black,
  
  table.header(
    [Header 1], [Header 2], [Header 3], [Header 4]
  ),

  [cell1,1], [cell1,2], [cell1,3], [cell1,4],
  [cell2,1], [cell2,2], [cell2,3], [cell2,4]
)
```

Pandoc copies the contents of the block directly into the generated `.typ` 
file. This can be used when Pandoc typst renderer does not fully output the 
desired markdown component