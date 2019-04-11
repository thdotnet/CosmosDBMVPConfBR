# CosmosDBMVPConfBR
Demo of Azure CosmosDB - Gremlin API

query samples:


//cast of matrix
`g.V('1').outE('starred').inV()`

//get all actors
`g.V().hasLabel('person').has('role', eq('actor'))`

//get all actress
`g.V().hasLabel('person').has('role', eq('actress'))`

//all movies before 2005
`g.V().hasLabel('movie').has('year', lt('2005'))`
