# CosmosDBMVPConfBR
Demo of Azure CosmosDB - Gremlin API

query samples:


GET all cast from the matrix movie

`g.V('1').outE('starred').inV()`

GET all actors

`g.V().hasLabel('person').has('role', eq('actor'))`

GET all actress

`g.V().hasLabel('person').has('role', eq('actress'))`

GET all movies before 2005

`g.V().hasLabel('movie').has('year', lt('2005'))`
