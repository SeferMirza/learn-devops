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
or package managers. For more information, view 
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

## Parsing `.md` files

Pandoc parses `.md` files by first converting them into an abstract syntax tree 
(AST) structure. Markdown content is arranged into structured elements like 
headings, paragraphs, lists, links, and code blocks. After the content is 
parsed into an AST, it can be converted to other file types such as HTML, PDF, 
or Word.

Use the following command to view the native AST representation of your `.md` 
file:

```bash
pandoc -f markdown -t native input.md
```

## Generating `.pdf` files

`Pandoc` requires an external PDF generation engine to produce PDF files. Common 
engines include `pdfLaTeX`, `LuaLaTeX`, and `typst`. In this project, `typst` 
is used for its simplicity and easy setup.

The command below generates a PDF directly from the `input.md` file:

```bash
# single line (direct PDF generation)
pandoc input.md -o output.pdf --pdf-engine=typst
```

It is also possible to  generate an intermediate `.typ` file and compile 
it manually:

```bash
pandoc input.md -t typst -o output.typ
typst compile output.typ output.pdf
```

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

### Filters

Pandoc includes built-in Lua filters which are tools for modifying a document’s 
structure during conversion.

For more information, visit: https://pandoc.org/filters.html

```bash
pandoc input.md -o output.pdf --lua-filter=table-filter.lua --pdf-engine=typst
```

### Typst


#### Template

