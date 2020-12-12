/// <reference path="knockout-3.5.1.debug.js" />

function person(completeName) {
    let self = this;

    self.fullName = ko.observable(completeName);
}

function databaseViewModel() {
    let self = this;

    //Getting data from server


    //self.dummyPersonData = [
    //    { id: 1, firstName: "John", lastName: "Doe" },
    //    { id: 2, firstName: "Tai", lastName: "Chan" },
    //    { id: 3, firstName: "Levi", lastName: "Kun" }
    //];

    //Adding data to display in the web page
    self.personList = ko.observableArray([
        new person(self.dummyPersonData[0]),
        new person(self.dummyPersonData[1]),
        new person(self.dummyPersonData[2])
    ]);

}

ko.applyBindings(new databaseViewModel());