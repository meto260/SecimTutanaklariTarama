
var parseActions = new ParseActions();
Console.WriteLine("App started");
Console.WriteLine("---------------------------------");
Console.WriteLine();

var gcpConfig = parseActions.ParseFromConfiguration();
var docProcess = new DocProcess(gcpConfig);

Console.WriteLine("File Path Waiting");
string locationInput = Console.ReadLine();
foreach(var file in Directory.GetFiles(locationInput)){
    ScanDocument(locationInput);
}

Console.WriteLine();
Console.WriteLine("---------------------------------");
Console.WriteLine("App finished");
Console.Read();


void ScanDocument(string localPath) {
    var gcpResponse = docProcess.CreateProcess(localPath);
    var parseResults = parseActions.ParseResultDocument(gcpResponse.Document.Text);
    foreach (var pr in parseResults) {
        foreach (var dr in pr) {
            Console.WriteLine(dr.Key + ":" + dr.Value);
        }
    }
}