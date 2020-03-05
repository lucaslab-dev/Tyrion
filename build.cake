#tool "nuget:?package=OpenCover&version=4.7.922"
#tool "nuget:?package=ReportGenerator&version=4.2.20"

var target = Argument("target", "Build");
var matrixmanifest = Argument("matrixmanifest", "manifest.yml");

Task("Build")
    .Does(() =>
{
    DotNetCoreBuild("./Tyrion.CQRS.sln");
});

Task("Test")
    .Does(() =>
{
    DotNetCoreTest("./Tyrion.CQRS.sln");
});

Task("Coverage")
    .Does(() =>
{
    OpenCover(tool =>
    {
        tool.DotNetCoreTest("./Tyrion.CQRS.sln");
    },
    new FilePath("./result.xml"),
    new OpenCoverSettings() { OldStyle = true, ReturnTargetCodeOffset = 0 }
        .WithFilter("-[Tyrion.Tests*]*")
        .WithFilter("+[Tyrion*]*")
        .WithFilter("-[Tyrion.Tests*]*")
    );

    ReportGenerator("./result.xml", "./coverageOutput", new ReportGeneratorSettings  { ReportTypes = new []
    {
        ReportGeneratorReportType.TextSummary,
        ReportGeneratorReportType.Html
    }});
});


RunTarget(target);