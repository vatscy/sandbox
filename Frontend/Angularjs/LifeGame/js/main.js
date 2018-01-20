/**
 * Created by k_naoya on 2013/11/09.
 */
function lifeController($scope) {
    $scope.size = 40;

    $scope.counter = {
        alive: 0,
        birth: 0,
        die: 0
    };

    var timer = null;

    var createField = function () {
        var arr = new Array($scope.size);
        for (var i = 0; i < arr.length; i++) {
            arr[i] = new Array($scope.size);
        }
        return arr;
    };

    $scope.destroy = function () {
        $scope.stop();
        $scope.counter.alive = 0;
        $scope.counter.birth = 0;
        $scope.counter.die = 0;
        for (var i = 0; i < $scope.lives.length; i++) {
            for (var j = 0; j < $scope.lives[i].length; j++) {
                $scope.lives[i][j] = false;
            }
        }
    };

    $scope.plant = function () {
        var loop = Math.floor($scope.size * $scope.size * Math.random() / 2);
        for (var l = 0; l < loop; l++) {
            var i = Math.floor(Math.random() * $scope.size);
            var j = Math.floor(Math.random() * $scope.size);
            if (!$scope.lives[i][j]) {
                $scope.lives[i][j] = true;
                $scope.counter.alive++;
                $scope.counter.birth++;
            }
        }
    };

    $scope.observe = function () {
        var isInRange = function (i, j) {
            return 0 <= i && i < $scope.size && 0 <= j && j < $scope.size;
        };

        var countAliveNeighbors = function (i, j) {
            var count = 0;
            // 左上
            if (isInRange(i - 1, j - 1) && $scope.lives[i - 1][j - 1]) {
                count++;
            }
            // 上
            if (isInRange(i - 1, j) && $scope.lives[i - 1][j]) {
                count++;
            }
            // 右上
            if (isInRange(i - 1, j + 1) && $scope.lives[i - 1][j + 1]) {
                count++;
            }
            // 左
            if (isInRange(i, j - 1) && $scope.lives[i][j - 1]) {
                count++;
            }
            // 右
            if (isInRange(i, j + 1) && $scope.lives[i][j + 1]) {
                count++;
            }
            // 左下
            if (isInRange(i + 1, j - 1) && $scope.lives[i + 1][j - 1]) {
                count++;
            }
            // 下
            if (isInRange(i + 1, j) && $scope.lives[i + 1][j]) {
                count++;
            }
            // 右下
            if (isInRange(i + 1, j + 1) && $scope.lives[i + 1][j + 1]) {
                count++;
            }
            return count;
        };

        var canBeAlive = function (i, j) {
            return isInRange(i, j)
                && (($scope.lives[i][j] && (countAliveNeighbors(i, j) == 2 || countAliveNeighbors(i, j) == 3))
                || (!$scope.lives[i][j] && countAliveNeighbors(i, j) == 3));
        };

        var nextLives = createField();
        for (var i = 0; i < $scope.size; i++) {
            for (var j = 0; j < $scope.size; j++) {
                nextLives[i][j] = canBeAlive(i, j);
                if ($scope.lives[i][j] && !nextLives[i][j]) {
                    $scope.counter.die++;
                    $scope.counter.alive--;
                } else if (!$scope.lives[i][j] && nextLives[i][j]) {
                    $scope.counter.birth++;
                    $scope.counter.alive++;
                }
            }
        }
        $scope.lives = nextLives;
    };

    $scope.interval = 250;

    $scope.start = function () {
        if (timer == null) {
            timer = setInterval(function () {
                $scope.observe();
                $scope.$apply();
            }, $scope.interval);
        }
    };

    $scope.stop = function () {
        if (timer != null) {
            clearInterval(timer);
            timer = null;
        }
    };

    $scope.lives = createField();
    $scope.destroy();
    $scope.plant();
}