function ContactViewViewModel(viewModel, _email, _First_Name, _Last_Name, _Phone_No) {
    var self = this;
    self.Email = ko.observable(_email);
    self.First_Name = ko.observable(_First_Name);
    self.Last_Name = ko.observable(_Last_Name);
    self.Phone_No = ko.observable(_Phone_No);
    self.Apps = ko.observableArray(viewModel);

    var ContactUri = '/api/ContactService/';
    var allApps;
    self.Allusers = function (appUsersVM) {
        $.ajax({
            type: "GET",
            url: 'api/ContactService/AllContacts/',
            contentType: "application/json; charset=utf-8", //set content type to application/json which will automatically return the value to json
            dataType: "json",
            success: function (response) {
                allApps = response;
                appUsersVM.Apps(allApps);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }


        });
        return false;

    }

   }