#set page(
  margin: 1cm
)

#let title = "Document Title"

#show table.cell.where(y: 0): set text(weight: "medium")
#show table.cell.where(x: 1): set text(weight: "bold")

#show heading: it => {
  set text(fill: rgb(0, 120, 200))
  it
}

#show table.cell.where(y: 0): set text(weight: "medium")

#set table(
  stroke: 0.5pt + black,
)

#title

$body$