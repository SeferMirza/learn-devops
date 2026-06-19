#set page(
  header: context {
    box(
      inset: (top: 6pt, bottom: 2pt),
      width: 100%,
      grid(
        columns: (2fr, 1fr),
        align: (left, right),
        [ #image("./assets/logo-full-secondary-150px.png", height: 5mm) ],
        [ #datetime.today().display("[year]-[month]-[day]") ]
      )
    )
  },
  footer: context {
    box(
      inset: (bottom: 6pt),
      width: 100%,
      grid(
        columns: (1fr, 1fr, 1fr),
        align: (left, center, right),
        [ #image("./assets/logo-full-secondary-150px.png", height: 2mm) ],
        [ Page #counter(page).display() ],
        [ Mouseless]
      )
    )
  },
  margin: 3cm
)

#let title = "Title From Template"

#show table.cell.where(y: 0): set text(weight: "medium")
#show table.cell.where(x: 1): set text(weight: "bold")

#show heading: it => {
  set text(fill: rgb(0, 120, 200))
  it
}

#show title: it => {
  set text(fill: rgb(0, 120, 200), size: 24pt)
  it
}


#title

$body$