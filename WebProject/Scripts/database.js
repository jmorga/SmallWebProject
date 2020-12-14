/// <reference path="knockout-3.5.1.debug.js" />

function person(completeName) {
    let self = this;

    self.id = ko.observable(completeName.id);
    self.firstName = ko.observable(completeName.firstName);
    self.lastName = ko.observable(completeName.lastName);

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
                return "fail";
            }
        });
    });
}

function databaseViewModel() {
    let self = this;

    self.personList = ko.observableArray();

    //Getting data from server
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