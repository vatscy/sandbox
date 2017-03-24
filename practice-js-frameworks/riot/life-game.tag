<life-game>
  <table>
    <tbody>
      <tr each='{row in lives}'>
        <td each='{value in row}' class='{alive: value}'></td>
      </tr>
    </tbody>
  </table>
  <button onclick='{plant}'>Plant</button>
  <button onclick='{clear}'>Clear</button>
  <button onclick='{observe}'>Observe</button>
  <button onclick='{start}'>Start</button>
  <button onclick='{stop}'>Stop</button>

  <style>
    life-game table {
      border-collapse: collapse;
      margin-left: auto;
      margin-right: auto;
    }
    life-game td {
      border: 1px #FF0044 solid;
      width: 10px;
      height: 10px;
    }
    life-game td.alive {
      background-color: #FF0044;
    }
  </style>

  var createSchale = () => {
    var schale = [];
    for (var i = 0; i < opts.height; i++) {
      schale[i] = new Array(Number(opts.width));
    }
    return schale;
  };

  this.clear = () => (this.lives = createSchale());

  this.plant = () => {
    var seeds = Math.floor(Math.random() * opts.width * opts.height / 2);
    for (var _ = 0; _ < seeds; _++) {
      var i = Math.floor(Math.random() * opts.height);
      var j = Math.floor(Math.random() * opts.width);
      this.lives[i][j] = true;
    }
  };

  this.observe = () => {
    var isInRange = (i, j) => (0 <= i && i < opts.height && 0 <= j && j < opts.width);

    var countAliveNeighbors = (i, j) => {
      var count = 0;
      // top left
      if (isInRange(i - 1, j - 1) && this.lives[i - 1][j - 1]) count++;
      // top
      if (isInRange(i - 1, j) && this.lives[i - 1][j]) count++;
      // top right
      if (isInRange(i - 1, j + 1) && this.lives[i - 1][j + 1]) count++;
      // left
      if (isInRange(i, j - 1) && this.lives[i][j - 1]) count++;
      // right
      if (isInRange(i, j + 1) && this.lives[i][j + 1]) count++;
      // bottom left
      if (isInRange(i + 1, j - 1) && this.lives[i + 1][j - 1]) count++;
      // bottom
      if (isInRange(i + 1, j) && this.lives[i + 1][j]) count++;
      // bottom right
      if (isInRange(i + 1, j + 1) && this.lives[i + 1][j + 1]) count++;

      return count;
    };

    var nextLives = createSchale();
    for (var i = 0; i < opts.height; i++) {
      for (var j = 0; j < opts.width; j++) {
        var life = this.lives[i][j];
        var aliveNeighbors = countAliveNeighbors(i, j);
        nextLives[i][j] = aliveNeighbors == 3 || (life && aliveNeighbors == 2);
      }
    }
    this.lives = nextLives;
  };

  var timer = null;
  this.start = () => {
    if (!timer) {
      timer = setInterval(() => {
        this.observe();
        this.update();
      }, Number(this.opts.interval));
    }
  };
  this.stop = () => {
    if (timer) {
      clearInterval(timer);
      timer = null;
    }
  };

  // init
  this.clear();
  console.log(this);
</life-game>
