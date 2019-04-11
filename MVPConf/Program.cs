using System;
using System.Threading.Tasks;
using Gremlin.Net;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;


namespace MVPConf
{
    class Program
    {
        private static string hostname = "";
        private static int port = 443;
        private static string authKey = "";
        private static string database = "db";
        private static string collection = "movies";


        static void Main(string[] args)
        {
            Console.WriteLine($"Enviando dados para o CosmosDB...{DateTime.Now}");
            Task.WaitAll(ExecuteAsync());
            Console.WriteLine($"Fim. {DateTime.Now}");
        }

        public static async Task ExecuteAsync()
        {
            var gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                                                            username: "/dbs/" + database + "/colls/" + collection, password: authKey);
            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                await gremlinClient.SubmitAsync<dynamic>("g.V().drop()");

                await gremlinClient.SubmitAsync<dynamic>("g.addV('movie').property('id', '1').property('name', 'The Matrix').property('year', '1999')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('movie').property('id', '2').property('name', 'Fight Club').property('year', '1999')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('movie').property('id', '3').property('name', 'John Wick').property('year', '2014')");

                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '99').property('name', 'Lana Wachowski').property('role', 'director')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '100').property('name', 'Lilly Wachowski').property('role', 'director')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '101').property('name', 'Keanu Reaves').property('role', 'actor')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '102').property('name', 'Carrie-Anne Moss').property('role', 'actress')");

                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '103').property('name', 'David Fincher').property('role', 'director')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '104').property('name', 'Helena Bonham Carter').property('role', 'actress')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '105').property('name', 'Edward Norton').property('role', 'actor')");
                await gremlinClient.SubmitAsync<dynamic>("g.addV('person').property('id', '106').property('name', 'Brad Pitt').property('role', 'actor')");

                await gremlinClient.SubmitAsync<dynamic>("g.V('1').addE('directedBy').to(g.V('99'))"); //The matrix -> Lana Wachowski (director)
                await gremlinClient.SubmitAsync<dynamic>("g.V('1').addE('directedBy').to(g.V('100'))"); //The matrix -> Lilly Wachowski (director)

                await gremlinClient.SubmitAsync<dynamic>("g.V('2').addE('directedBy').to(g.V('103'))"); //Fight Club -> David Fincher (director)
                await gremlinClient.SubmitAsync<dynamic>("g.V('2').addE('starred').to(g.V('105'))"); //Fight Club -> Edward Norton (actor)
                await gremlinClient.SubmitAsync<dynamic>("g.V('2').addE('starred').to(g.V('106'))"); //Fight Club -> Brad Pitt (actor)
                await gremlinClient.SubmitAsync<dynamic>("g.V('2').addE('starred').to(g.V('104'))"); //Fight Club -> Helena Bonham Carter (actress)

                await gremlinClient.SubmitAsync<dynamic>("g.V('1').addE('starred').to(g.V('101'))"); //The matrix -> Keanu Reaves (actor)
                await gremlinClient.SubmitAsync<dynamic>("g.V('1').addE('starred').to(g.V('102'))"); //The matrix -> Carrie-Anne Moss (actress)

                await gremlinClient.SubmitAsync<dynamic>("g.V('3').addE('starred').to(g.V('101'))"); //John Wick -> Keanu Reaves (actor)
            }
        }
    }
}
