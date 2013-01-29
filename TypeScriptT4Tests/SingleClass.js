(function (Module2) {
    (function (Module1) {
        var Class1 = (function () {
            function Class1() {
            }
            Class1.prototype.function1 = function () {
            };
            return Class1;
        })();
        Module1.Class1 = Class1;        
    })(Module2.Module1 || (Module2.Module1 = {}));
    var Module1 = Module2.Module1;
})(exports.Module2 || (exports.Module2 = {}));
var Module2 = exports.Module2;
