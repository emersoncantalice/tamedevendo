var ViewModel = function () {
    var self = this;
    self.dividas = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.clientes = ko.observableArray();
    self.newDivida = {
        Cliente: ko.observable(),
        Preco: ko.observable(),
        Descricao: ko.observable(),
        Data: ko.observable(),
        Status: ko.observable()
    };
    self.newCliente = {
        Nome: ko.observable(),
        Telefone: ko.observable()
    };
    self.total = 0;


    var dividasUri = '/api/dividas/';
    var clientesUri = '/api/clientes/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllDividas() {
        ajaxHelper(dividasUri, 'GET').done(function (data) {
            self.dividas(data);
        });
    }

    self.getDividaDetail = function (item) {
        ajaxHelper(dividasUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function getClientes() {
        ajaxHelper(clientesUri, 'GET').done(function (data) {
            self.clientes(data);
        });
    }

    function getTotalAReceber() {

        ajaxHelper(dividasUri + "total/", 'GET').done(function (data) {
            self.total = data;
        });

    }


    self.addDivida = function (formElement) {
        var divida = {
            ClienteId: self.newDivida.Cliente().Id,
            Preco: self.newDivida.Preco(),
            Descricao: self.newDivida.Descricao(),
            Data: self.newDivida.Data(),
            Status: self.newDivida.Status()
        };

        ajaxHelper(dividasUri, 'POST', divida).done(function (item) {
            self.dividas.push(item);
        });
       
    }

    self.addCliente = function (formElement) {
        var cliente = {
            Nome: self.newCliente.Nome(),
            Telefone: self.newCliente.Telefone()
        };

        ajaxHelper(clientesUri, 'POST', cliente).done(function (item) {
            self.clientes.push(item);
        });
    }

    // Fetch the initial data.
    getAllDividas();
    getClientes();
    getTotalAReceber();
};

ko.applyBindings(new ViewModel());