# Constant Contact .NET SDK API v. 3

This SDK currently allows you:
- Create custom emails campaigns
- Renew Access Token

**Please feel free to collaborate and add missing features.**

## Get initial Access Token

Before using CTCTv3 you need to get AccessToken.
To get INITIAL Access Token, follow the instructions at https://developer.constantcontact.com/api_guide/server_flow.html (you can do it without any code, with just browser + curl command-line tool).  

After you have the initial Access and Refresh token, you can use CTCTv3 to renew it (so you never need to get tokens manually again).

## Usage

0. Create ConstantContact object:

```
var constantContact = new ConstantContact("<YOUR ACCESS TOKEN>");
```

### Renew Access Token

```
var data = constantContact.RefreshAccessToken("<YOUR APP CLIENT ID>", "<YOUR APP CLIENT SECRET>", "<REFRESH TOKEN>");

// The method returns new Access and Refresh tokens. You should save them overriding existing expired tokens:
var newAccessToken = data.AccessToken;
var newRefreshToken = data.RefreshToken;
```

### Create the new email campaign

This is the example how to create and send new Custom Code email using API v.3:

1. Create campaign object:

```
var campaign = new EmailCampaign()
{
  Name = "Email campaign name",
  EmailCampaignActivities = new List<EmailCampaignActivity>()
    {
      new EmailCampaignActivity()
      {
        Subject = "subject",
        FromEmail = "from@gmail.com",
        ReplyToEmail = "from@gmail.com",
        FromName = "John Smith",
        HtmlContent = "<body>Your email content goes here</body>",
        MessageFooter = new MessageFooter()
        {
          OrganizationName = "My company",
          AddressLine1 = "256 Programmers street",
          AddressLine2 = "Apt #16",
          City = "Philadelphia",
          StateCode = "PA",
          PostalCode = "19000",
          CountryCode = "US"
        }
      }
    }
};
```

2. Add campaign. This will create DRAFT Campain in Constant Contact

```
campaign = constantContact.AddCampaign(campaign);
```

3. Now add contacts to your campaing. I assume you aleady have the list of contacts in CC.

  - 3a. Retrieve the list:
```
var contactList = constantContact.GetList("<NAME OF YOUR LIST>");
```

  - 3b. Add contact list ID to your campaign. This will make your campaign email to send to all the recepients in the selectedcontact list:

```
campaign.GetPrimaryActivity().ContactListIds = new string[] { contactList.Id };
```

  - 3c. Update campaign

```
var primaryActivity = constantContact.UpdateCampaign(campaign);
```

4. Schedule the campaign to be send:
			
Option 1. Schedule to send in 5 minutes from now:
```
constantContact.ScheduleToSend(primaryActivity.Id, DateTime.Now.AddMinutes(5));
```

Option 2. Schedule to send immidiately:

```
constantContact.ScheduleToSendImmidiately(primaryActivity.Id);
```

