var app = angular.module("myApp", []);
/*
.config(function ($sceProvider) {
    // Completely disable SCE.  For demonstration purposes only!
    // Do not use in new projects.
    $sceProvider.enabled(false);
});
*/


app.service("mediaSrvc", ["$http", function ($http) {
    this.createMedia = function (link) {
        return $http({
            url: link,
            method: "Post"
        });
    }
}]);

app.controller("myCtrl", ["$scope", "mediaSrvc", function ($scope, mediaSrvc) {
    //获取主机名
    var hostname = location.hostname;
    //获取端口号
    var port = location.port;
    //获取主机名+端口号
    var host = location.host;

    $scope.media = [];
    $scope.getMedia = function () {
        var msg = $scope.input;
        mediaSrvc
            .createMedia("http://" + host + "/cryout?message=" + msg)
            .success(function (data, status, headers, config) {
                $scope.media.unshift({
                    filePath: "http://" + host + "/media?FileName=" + data,
                    message: msg
                });
            });
    }

}]);