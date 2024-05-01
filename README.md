# Claims_Api

To run SwaggerUI, select **Http** when debugging the project.

There are four total endpoints:
- `Claim/company-claims`: gets the claims associated against a provided company;
- `Claim/claim-details`: gets the details for a particular claim;
- `Claim/update-claim`: updates a given claim;
- `Company/company-details`: gets the details for a particular company.

I have added some seed data for both the ClaimController and CompanyController which obviously would not exist in production but have been included for testing purposes.

Whilst the unit tests are not exhaustive, they give decent coverage of the API functionality.

I would have included authentication and DTOs with more information provided/more time to implement.