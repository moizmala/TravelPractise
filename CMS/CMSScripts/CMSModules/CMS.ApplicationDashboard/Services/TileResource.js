define(["require", "exports", 'CMS/Application'], function(require, exports, application) {
    exports.Resource = [
        '$resource',
        function ($resource) {
            var saveAction = {
                method: 'POST',
                transformRequest: function (applications) {
                    var guids = applications.map(function (app) {
                        return app.Guid;
                    });
                    return JSON.stringify(guids);
                }
            };

            return $resource(application.getData('applicationUrl') + 'cmsapi/Tile/', {}, {
                save: saveAction
            });
        }
    ];
});
