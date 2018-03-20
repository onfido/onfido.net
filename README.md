:warning: **This project is not offcially supported or maintained by Onfido**.  It is available to use but we are unable to provide assistance for any technical queries.

# Onfido.NET [![Build status](https://ci.appveyor.com/api/projects/status/tyl4dpdieme18acv/branch/master?svg=true)](https://ci.appveyor.com/project/morganjbruce/onfido-net-890c6/branch/master)

Onfido.NET is a .NET API client for Onfido's REST API.

# Installation

You can obtain Onfido.NET from [nuget](https://www.nuget.org/packages/Onfido.NET) by using the Package Browser, or the Package Manager Console:

    PM> Install-Package Onfido.NET

# Usage/Setup

To setup your API token you'll need to add a call to ```Onfido.Settings.SetApiToken()``` somewhere in your application.

    Onfido.Settings.SetApiToken("whatever_your_token_is");

Optionally, you may want to set the version of the API that you're calling; this defaults to the latest version of the API.

    Onfido.Settings.SetApiVersion("v2");

When calling each of the services you can either create an instance of ```Onfido.Api``` - which will allow you to access all the Onfido endpoints from a single object - or you can create separately as required. For each of the examples below, where we have something like

    var Applicant = new Onfido.Resources.Applicants();
    return Applicant.Create(applicant)

the following is equally valid

    var api = new Onfido.Api();
    return api.Applicants.Create(applicant);    

## Applicants

The [Applicants](https://onfido.com/documentation#applicants) endpoint supports three operations - ``Create()``, ``Find()``, and ``All()``:

### Create

    var applicants = new Onfido.Resources.Applicants();
    var applicant = new Applicant {
    	FirstName = "John",
    	LastName = "Smith"
    	// ...
    };

	var newApplicant = applicants.Create(applicant);

### Retrieve

    var applicants = new Onfido.Resources.Applicants();
    var existingApplicant = applicants.Find(applicantId);

### All

    var applicants = new Onfido.Resources.Applicants();
    var allApplicants = applicants.All();

The ``All()`` operation also permits pagination

    var applicants = new Onfido.Resources.Applicants();
    var top10Applicants = applicants.All(1, 10):
    var next10Applicants = applicants.All(2, 10):

## Documents

The [Documents](https://onfido.com/documentation#documents) endpoint supports a single operation - ``Create()``:

### Create

    var documents = new Onfido.Resources.Documents();
    var passport = new FileStream(@"./passport.png", FileMode.Open);
    var document = api.Documents.Create(applicantId, passport, "passport.png", Entities.DocumentType.Passport);

Since you can optionally supply a side, ``Create()`` also allows you to specify which side of a document you're uploading

    var documents = new Onfido.Resources.Documents();
    var passport = new FileStream(@"./passport.png", FileMode.Open);
    var document = api.Documents.Create(applicantId, passport, "passport.png", Entities.DocumentType.Passport, Entities.DocumentSide.Front);

The ``Entities.DocumentType`` and ``Entities.DocumentSide`` enums support the following:

    public enum DocumentType {
        Passport,
        NationalIdentityCard,
        WorkPermit,
        DrivingLicense,
        NationalInsurance,
        BirthCertificate,
        BankStatement,
        Unknown
    }

    public enum DocumentSide
    {
        Front,
        Back
    }

## Checks

The [Checks](https://onfido.com/documentation#checks) endpoint supports three operations - ``Create()``, ``Find()`` and ``All()``:

### Create

    var checks = new Onfido.Resources.Checks();
    var check = new Check
            {
                Type = CheckType.Standard,
                Reports = new List<Report>
                {
                    new Report { Name = "identity" },
                    new Report { Name = "document" }
                }
            };
    var new_check = checks.Create(applicant_id, check);

### Find

    var checks = new Onfido.Resources.Checks();
    var existingCheck = checks.Find(applicantId, checkId);

### All

    var checks = new Onfido.Resources.Checks();
    var applicantChecks = checks.All(applicantId);

The ``All()`` operation also permits pagination

    var checks = new Onfido.Resources.Checks();
    var top10Checks = checks.All(1, 10):
    var next10Checks = checks.All(2, 10):

## Reports

The [Reports](https://onfido.com/documentation#reports) endpoint supports two operations - ``Find()`` and ``All()``:

### Find

    var reports = new Onfido.Resources.Reports();
    var existingReport = reports.Find(checkId, reportId);

### All
    var reports = new Onfido.Resources.Reports();
    var allReports = reports.All(checkId);
