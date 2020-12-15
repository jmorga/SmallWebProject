/// <reference path="knockout-3.5.1.debug.js" />

//Person class to store the id, first name and last name of a person.
function person(completeName) {
    let self = this;

    self.id = ko.observable(completeName.id);
    self.firstName = ko.observable(completeName.firstName);
    self.lastName = ko.observable(completeName.lastName);

    //When the first or last name changes, an ajax call is used to send the new information to the ChangePeople 
    //method in the Database controller to update the Person table in the People database with the new information.
    ko.computed(function () {

        $.ajax({
            type: "POST",
            url: "/Database/ChangePeople",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                'id': self.id(),
                'firstName': self.firstName(),
                'lastName': self.lastName()
            }),
            dataType: "json",
            success: function (data) {
                console.log("Saved");
            },
            error: function () {
                alert("Saving data failed");
            }
        });
    });
}

function databaseViewModel() {
    let self = this;

    self.personList = ko.observableArray();

    //It calls the GetPeople method from the database controller to get an array with the data from
    //the Person table in the People database
    $.ajax({
        type: "POST",
        url: "/Database/GetPeople",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: updateTable,
        error: function () {
            alert("Fail");
        }
    });


    //When the ajax call gets the data, it calls this function to add the data into the list to be displayed in the web page
    function updateTable(data) {
        data.forEach(function (entry) {
            self.personList.push(new person(entry));
        });
    };

    //self.dummyPersonData = [
    //    { id: 1, firstName: "John", lastName: "Doe" },
    //    { id: 2, firstName: "Tai", lastName: "Chan" },
    //    { id: 3, firstName: "Levi", lastName: "Kun" }
    //];

    //Adding data to display in the web page
    //self.personList = ko.observableArray([
    //    new person(self.dummyPersonData[0]),
    //    new person(self.dummyPersonData[1]),
    //    new person(self.dummyPersonData[2])
    //]);

}

ko.applyBindings(new databaseViewModel());