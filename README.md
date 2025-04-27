# Ignite Medication Request API

This is a simple API as a proof of concept for Ignite. It is built with .NET 8 and EF 9


## Setup
After cloning the repo, you will then need to modify the appsettings.development.json file. Update the PostgresDb connection string to use your local IP address (192.168.X.Y). For simplicity of this demo app, I have included the postgres password in the connection string already.

Install the dotnet ef CLI tools if needed ("dotnet tool install --global dotnet-ef --version 9.*")

Within the nested "Ignite.MedicationRequest.API" folder, run "dotnet ef database update" to create the initial postgres DB schema.

To run the app, ensure that Docker Desktop is running, and that the docker-compose project is the default project. Then click run (f5) in Visual Studio.

If the container is running, tests can be executed by the Visual Studio runner.

## About the implementation
When creating a medication request, it must refer to a correct Medication, Patient, and Clinician. I have seeded the DB with an initial Clinician, Patient, and Medication. These each have an Id of 1.

## Notes

Some assumptions have been made:

- It felt right to me that MedicationRequests always existed within the context of a patient, hence why my route structure is /patients/{patientId}/medicationRequests

- I chose to use FluentValidation to demo how I typically handle more complex validation scenarios, although in general I kept the validation quite light to avoid spending too much time on it.

- I made some type assumptions for properties such as the Clinician > Registration Id


## Potential Improvements

There's a lot of potential improvements that could be made to the solution, but given the time constraints I chose not to implement them:

Some examples:
	- Handling Enum values in a better way (e.g. using JsonStringEnumConverter to make the enum values for Medication > Form a bit easier to understand)
	- Generic error handling middleware
	- Health checks
	- Cancellation tokens
	- Logging
	- Perhaps creating a reusable object for coded values (as coded values are often used across FHIR entities)
	- I could consider creating a generic repository interface that could be shared across repos
	- Improved validation