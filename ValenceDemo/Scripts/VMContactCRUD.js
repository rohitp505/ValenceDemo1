function ContactViewViewModel(viewModel, _email, _First_Name, _Last_Name, _Phone_No) {
    var self = this;
    self.Email = ko.observable(_email);
    self.First_Name = ko.observable(_First_Name);
    self.Last_Name = ko.observable(_Last_Name);
    self.Phone_No = ko.observable(_Phone_No);
    self.Apps = ko.observableArray(viewModel);
    self.Allusers = function (appUsersVM) {
        selectOpertion();
        GetAll(appUsersVM);

    }


    //Calling all crud method from one button only
    self.CRUD = function (appUsersVM) {

        if ($("#btnAction").val() == "Create") {
            if(self.Email() == null || self.First_Name() == null || self.Last_Name() == null || self.Phone_No().length<10)
            {
                alert("invalid data");
                return;
            }
            if (self.Phone_No().length <10  ) {
                alert(self.Phone_No()+" invalid Phone No.");
                return;
            }
            Create(appUsersVM, self);
        }
        if ($("#btnAction").val() == "Read") {
            alert(self.Email());
           if(self.Email().length<3 )
            {
                alert("invalid data");
                return;
            }
            
            Read(appUsersVM, self.Email());
        }
        if ($("#btnAction").val() == "Update") {
            if(self.Email() == null || self.First_Name() == null || self.Last_Name() == null || self.Phone_No().length<10)
            {
                alert("invalid data");
                return;
            }
            if (self.Phone_No().length > 11) {
                alert("invalid Phone No.");
                return;
            }
             Update(appUsersVM, self.Email(), self);
             
        }
        if ($("#btnAction").val() == "Delete") {
            if(self.Email() == null )
            {
                alert("invalid data");
                return;
            }
            
            Delete(appUsersVM, self.Email());
            self.clearFields();
        }

        self.Allusers;
    }


    //Clearing the fields
    self.clearFields = function clearFields() {
        self.First_Name('');
        self.Last_Name('');
        self.Email('');
    }

   
  
      
  
    

    


}
var ContactUri = '/api/ContactService/';
function GetAll(appUsersVM) {
    var allApps;
    $.ajax({
        type: "GET",
        url: ContactUri,
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


function Create(appUsersVM, data) {
    var allApps;
    $.ajax({
        type: "Post",
        url: ContactUri,
        contentType: "application/json; charset=utf-8", //set content type to application/json which will automatically return the value to json
        data: ko.toJSON(data),
        dataType: "json",
        success: function (response) {
            allApps = response;
            appUsersVM.Apps(allApps);
            alert(data.Email+" Created Succesfully!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }


    });
    return false;
}


function Read(appUsersVM, ids) {
    var allApps;
    $.ajax({
        type: "GET",
        url: ContactUri,
        //contentType: "application/json; charset=utf-8", //set content type to application/json which will automatically return the value to json
        data: { id: ids },
        dataType: "json",
        success: function (response) {
            allApps = response;
            appUsersVM.Apps(allApps);
            appUsersVM.First_Name(response.First_Name);
            appUsersVM.Last_Name(response.Last_Name);
            appUsersVM.Phone_No(response.Phone_No);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }


    });
    return false;
}


function Update(appUsersVM, ids, data) {
    var allApps;
    $.ajax({
        type: "Put",
        url:ContactUri,
        contentType: "application/json; charset=utf-8", //set content type to application/json which will automatically return the value to json
        //data: { 'id': ids, 'contactdetail': ko.toJSON(data) },
        data: ko.toJSON(data),
        dataType: "json",
        success: function (response) {
            alert("Update Succesfully !");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }


    });
    return false;
}


function Delete(appUsersVM, ids) {
    var allApps;
    $.ajax({
        url:ContactUri ,
        type: 'PUT',
        dataType: 'json',
        data: ids,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            allApps = response;
            appUsersVM.Apps(allApps);
            alert("Deletion Done Succesfully!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        }


    });
    return false;
}


//Enable desable the controls according crud selection
function selectOpertion() {


    if (document.getElementById('isCreate').checked) {
        document.getElementById('emaildiv').style.pointerEvents = 'all';
        document.getElementById('exceptEmaildiv').style.pointerEvents = 'all';
        $("#btnAction").val("Create");
    }
    if (document.getElementById('isRead').checked) {
        document.getElementById('emaildiv').style.pointerEvents = 'all';
        document.getElementById('exceptEmaildiv').style.pointerEvents = 'none';
        $("#btnAction").val("Read");
    }
    if (document.getElementById('isUpdate').checked) {
        document.getElementById('emaildiv').style.pointerEvents = 'none';
        document.getElementById('exceptEmaildiv').style.pointerEvents = 'all';
        $("#btnAction").val("Update");
    }
    if (document.getElementById('isDelete').checked) {
        document.getElementById('emaildiv').style.pointerEvents = 'all';
        document.getElementById('exceptEmaildiv').style.pointerEvents = 'none';
        $("#btnAction").val("Delete");
    }


}

 
