(function () {
    "use strict";

    var App = angular.module("App", []);

    App.factory('dataFactory', ['$http', function ($http) {
        var urlBase = 'http://localhost:50924/api';
        var dataFactory = {};

        dataFactory.getCategories = function () {
            var urlLocal = '/AllCategories';
            return $http.get(urlBase + urlLocal);
        };

        dataFactory.getProdutos = function () {
            var urlLocal = '/AllProducts';
            return $http.get(urlBase + urlLocal);
        };

        dataFactory.getProdutosName = function (name) {
            var urlLocal = '/AllProductsForNome';
            return $http.get(urlBase + urlLocal + '/' + name);
        };

        dataFactory.AddProduct = function (product) {
            var urlLocal = '/AddProduct';
            return $http.post(urlBase + urlLocal, product);
        }

        dataFactory.SaveProduct = function (Id, product) {
            var urlLocal = '/SaveProduct';
            return $http({ url: urlBase + urlLocal + '/' + Id, method: 'PUT', data: product });
        }

        dataFactory.EditProduct = function (Id) {
            var urlLocal = '/ProductForId';
            return $http.get(urlBase + urlLocal + '/' + Id);
        }

        dataFactory.EraseProduct = function (Id) {
            var urlLocal = '/EraseProduct';
            return $http({ url: urlBase + urlLocal + '/' + Id, method : 'DELETE' });
        }
        return dataFactory;
    }]);

    App.controller("AppController", ["$scope", "dataFactory", function ($scope, dataFactory) {
    }]);

    App.controller("HomeController", ["$scope", "dataFactory", function ($scope, dataFactory) {
        $scope.product = { Id: 0, Name: '', CategoryId: 0, Price: 0.00 };
        $scope.prodFilter = '';
        $scope.listProd = [];
        $scope.listCate = [];
        $scope.frmShow = false;
        $scope.lstShow = true;
        $scope.btnAddShow = false;

        getCategories();
        getProducts();

        function getCategories() {
            dataFactory.getCategories()
                .then(function (response) {
                    $scope.listCate = response.data;
                }, function (error) {
                    setMsg(error.statusText, 'danger');
                });
        };

        function getProducts(name) {
            if ((name !== undefined) && (name !== '')){
                dataFactory.getProdutosName(name)
                    .then(function (response) {
                        $scope.listProd = response.data;
                    }, function (error) {
                        setMsg(error.statusText, 'danger');
                    });
            
            } else {
                dataFactory.getProdutos()
                    .then(function (response) {
                        $scope.listProd = response.data;
                    }, function (error) {
                        setMsg(error.statusText, 'danger');
                    });
            }
        };

        $scope.btnFilter = function () {
            getProducts($scope.prodFilter);
        };

        $scope.btnForm = function () {
            $scope.lstShow = false;
            $scope.frmShow = true;
            $scope.product = {};
            if ($scope.Id !== 0) {
                $scope.btnAddShow = true;
            };
        };

        $scope.btnSearch = function () {
            console.log('filtro', $scope.prodFilter);
            if ($scope.prodFilter !== '') {
                getProducts($scope.prodFilter);
            } else {
                getProducts();
            }
            $scope.lstShow = true;
            $scope.frmShow = false;
            $scope.btnAddShow = false;
        };

        $scope.btnSave = function () {
            if ((angular.isUndefined($scope.product.Name) === true) ||
                (angular.isUndefined($scope.product.CategoryId) === true) ||
                (angular.isNumber($scope.product.Price) === false)
            ) {
                return false;
            } else {
                $scope.product.error = '';
            };
                        
            if (($scope.product.Id === 0) || ($scope.product.Id === undefined)) {
                dataFactory.AddProduct($scope.product).then(function (response) {
                    setMsg(response.data.Message, 'success');
                });
            } else {
                dataFactory.SaveProduct($scope.product.Id, $scope.product).then(function (response) {
                    setMsg(response.data.Message, 'success');
                });
            }
        };

        $scope.btnEdit = function (Id) {
            dataFactory.EditProduct(Id).then(function (response) {
                $scope.lstShow = false;
                $scope.frmShow = true;
                $scope.product = response.data;
            });
        };

        $scope.btnErase = function (Id) {
            dataFactory.EraseProduct(Id).then(function (response) {
                setMsg(response.data.Message, 'success');
                getProducts();
                $scope.lstShow = true;
                $scope.frmShow = false;
                $scope.btnAddShow = false;
            });
        };
    }]);

    App.controller("PersonController", ["$scope", "dataFactory", function ($scope, dataFactory) {
        $scope.person = { Id: 0, namePerson: ''};
        $scope.prodFilter = '';
        $scope.listPers = [];
        $scope.frmShow = false;
        $scope.lstShow = true;
        $scope.btnAddShow = false;

    }]);


    function setMsg(msg, opc) {
        if (opc == 'success') {
            $("#ajaxmsgsucesso").removeClass('hidden');
            $('#ajaxsucesso').html(msg);
            setTimeout(function () {
                $("#ajaxmsgsucesso").addClass('hidden');
                $('#ajaxsucesso').html();
            }
                , 1000);
        } else if (opc == 'danger') {
            $("#ajaxmsgerro").removeClass('hidden');
            $('#ajaxerrors').html(msg);
            setTimeout(function () {
                $("#ajaxmsgerro").addClass('hidden');
                $('#ajaxmsgerro').html();
            }
                , 3000);
        }
        return false;
    };

})();