function IchthyologyTranscribeViewModel(rawModel) {
    var self = this;

    self.number = ko.observable(rawModel.Number).extend({ asTranscriptionField: true });
    self.scientificName = ko.observable(rawModel.ScientificName).extend({ asTranscriptionField: true });
    self.family = ko.observable(rawModel.Family).extend({ asTranscriptionField: true });
    self.commonName = ko.observable(rawModel.CommonName).extend({ asTranscriptionField: true });
    self.drainage = ko.observable(rawModel.Drainage).extend({ asTranscriptionField: true });
    self.locality = ko.observable(rawModel.Locality).extend({ asTranscriptionField: true });
    self.collectedBy = ko.observable(rawModel.CollectedBy).extend({ asTranscriptionField: true });
    self.date = ko.observable(rawModel.Date).extend({ asTranscriptionField: true });
    self.specimens = ko.observable(rawModel.Specimens).extend({ asTranscriptionField: true });
    self.fluid = ko.observable(rawModel.Fluid).extend({ asTranscriptionField: true });
    self.donor = ko.observable(rawModel.Donor).extend({ asTranscriptionField: true });
    self.accessNumber = ko.observable(rawModel.AccessNumber).extend({ asTranscriptionField: true });
}

function HerpetologyTranscribeViewModel(rawModel) {
    var self = this;

    self.number = ko.observable(rawModel.Number).extend({ asTranscriptionField: true });
    self.scientificName = ko.observable(rawModel.ScientificName).extend({ asTranscriptionField: true });
    self.commonName = ko.observable(rawModel.CommonName).extend({ asTranscriptionField: true });
    self.sex = ko.observable(rawModel.Sex).extend({ asTranscriptionField: true });
    self.locality = ko.observable(rawModel.Locality).extend({ asTranscriptionField: true });
    self.collector = ko.observable(rawModel.Collector).extend({ asTranscriptionField: true });
    self.date = ko.observable(rawModel.Date).extend({ asTranscriptionField: true });
    self.specimens = ko.observable(rawModel.Specimens).extend({ asTranscriptionField: true });
    self.measurements = ko.observable(rawModel.Measurements).extend({ asTranscriptionField: true });
    self.habitat = ko.observable(rawModel.Habitat).extend({ asTranscriptionField: true });
    self.temperature = ko.observable(rawModel.Temperature).extend({ asTranscriptionField: true });
    self.water = ko.observable(rawModel.Water).extend({ asTranscriptionField: true });
    self.time = ko.observable(rawModel.Time).extend({ asTranscriptionField: true });
    self.remarks = ko.observable(rawModel.Remarks).extend({ asTranscriptionField: true });
    self.fluid = ko.observable(rawModel.Fluid).extend({ asTranscriptionField: true });
    self.donor = ko.observable(rawModel.Donor).extend({ asTranscriptionField: true });
    self.accessNumber = ko.observable(rawModel.AccessNumber).extend({ asTranscriptionField: true });
}