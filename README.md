### Sprint Three
## Notes
- New UI update, (Using AI)
- Stripe Payment added

## Setting up
- Add the SMTP settings
- Add the Stripe secret key (Strip:SecretKey)


### Sprint Two

## Setting up configurations
- [ ] Set up the connection string
- [ ] Set up SMTP mail settings for email confirmation (implements options pattern, `MailSetting` model in `Models/Settings/`)


# Project Fixes List

Some fixes need to done in the last sprint, I grouped them in this short list.

## Security stuff
- [x] Make `CategoryDTO` instead of passing the category entity directly
- [x] Remove the "Register as Admin" link
- [x] Add explicit `[Authorize(Roles = "...")]` on the admin actions instead of just trusting the global fallback policy
- [x] Fix N+1 query problem happening in User Management

## Making the admin panel look consistent
- [x] Make Category and User Management using the same admin dashboard layout as Product
- [x] Use the same libraries that are used in the user management:
  - DataTables
  - SweetAlert2
  - Tostar

## Build the UI, instead of the default home/index
- [x] Build a real homepage where customers can browse products
- [x] Build enhanced pagination, e.g. `1 ... 5 6 7 ... 10`, instead of listing every page number e.g. `1 2 3 4 5 6 7 8 9 10...20`