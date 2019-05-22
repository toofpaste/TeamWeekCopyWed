var game = new Phaser.Game(400, 490, Phaser.AUTO, "gameDiv");


var mainState = {

    preload: function() {
        if(!game.device.desktop) {
            game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
            game.scale.setMinMax(game.width/2, game.height/2, game.width, game.height);
        }

        game.scale.pageAlignHorizontally = true;
        game.scale.pageAlignVertically = true;

        //game.stage.backgroundColor = '#71c5cf';
        game.load.image('pipe', 'assetsFlap/this.png');
        game.load.spritesheet('copter', 'assetsFlap/fire.png', 64, 64, 60);
        this.game.load.image('bg1', 'assetsFlap/1.png');
        this.game.load.image('bg2', 'assetsFlap/2.png');
        this.game.load.image('bg3', 'assetsFlap/3.png');
        this.game.load.image('bg4', 'assetsFlap/4.png');
        this.game.load.image('bg5', 'assetsFlap/5.png');
        this.game.load.image('bg6', 'assetsFlap/6.png');
        this.game.load.image('bg7', 'assetsFlap/7.png');
        this.game.load.image('bg8', 'assetsFlap/8.png');
        this.game.load.image('bg9', 'assetsFlap/9.png');
        this.game.load.image('bg10', 'assetsFlap/10.png');
        this.game.load.image('bg11', 'assetsFlap/11.png');
        //game.load.spritesheet('fire', 'assetsFlap/fire.png', 57, 192, 3);
        //57 192
        //http://www.lessmilk.com/tutorial/flappy-bird-phaser-1
        // Load the jump sound
        game.load.audio('jump', 'assetsFlap/Suck.wav');
        game.load.audio('death', 'assetsFlap/Death.wav');
        //game.load.audio('track', 'assets/track.wav');


    },


    create: function() {

        this.background1 = this.game.add.sprite(0, 0, 'bg11');
        this.background2 = this.game.add.sprite(0, -600, 'bg10'﻿);﻿
        this.background3 = this.game.add.sprite(0, -600, 'bg9'﻿);﻿
        this.background4 = this.game.add.sprite(0, -600, 'bg8'﻿);﻿
        this.background5 = this.game.add.sprite(0, -600, 'bg7'﻿);﻿
        this.background6 = this.game.add.sprite(0, -600, 'bg6'﻿);﻿
        this.background7 = this.game.add.sprite(0, -600, 'bg5'﻿);﻿
        this.background8 = this.game.add.sprite(0, -600, 'bg4'﻿);﻿
        this.background9 = this.game.add.sprite(0, -600, 'bg3'﻿);﻿
        this.background10 = this.game.add.sprite(0, -600, 'bg2'﻿);﻿
        this.background11 = this.game.add.sprite(0, -600, 'bg1'﻿);﻿
        game.physics.startSystem(Phaser.Physics.ARCADE);

        this.pipes = game.add.group();

        this.timer = game.time.events.loop(1500, this.addRowOfPipes, this);

        this.bird = game.add.sprite(100, 245, 'copter');
        var idle = this.bird.animations.add('idle');
        this.bird.scale.setTo(0.9, 0.9);
        this.bird.animations.play('idle',  30, true);
        // this.bird.debug.body(this.bird);
        game.physics.arcade.enable(this.bird);
        this.bird.body.gravity.y = 1000;

        // New anchor position
        this.bird.anchor.setTo(-0.2, 0.5);

        var spaceKey = game.input.keyboard.addKey(Phaser.Keyboard.SPACEBAR);
        spaceKey.onDown.add(this.jump, this);
        game.input.onDown.add(this.jump, this);



        this.score = 0;
        this.labelScore = game.add.text(20, 20, "0", { font: "30px Arial", fill: "#ffffff" });

        // Add the jump sound
        // game.sound.stopAll();
        this.jumpSound = game.add.audio('jump');
        this.jumpSound.volume = 0.2;
        this.deathSound = game.add.audio('death');
        this.deathSound.volume = 0.2;
        // this.trackSound = game.add.audio('track');
        // this.trackSound.volume = 0.3;
        // this.trackSound.play();
    },

    update: function() {
          var moveBackground = function(background)
          {
            if (background.x > 400) {
                 background.x = 400;
                 background.x -= 1;
                 }
            background.x -=1;
          };
          var moveBackground2 = function(background)
          {
            if (background.x > 400) {
                 background.x = 400;
                 background.x -= 2;
                 }
            background.x -=2;
          };
          var moveBackground3 = function(background)
          {
            if (background.x > 400) {
                 background.x = 400;
                 background.x -= 3;
                 }
            background.x -=3;
          };
          var moveBackground4 = function(background)
          {
            if (background.x > 400) {
                 background.x = 400;
                 background.x -= 4;
                 }
            background.x -=4;
          };
          var moveBackground5 = function(background)
          {
            if (background.x > 400) {
                 background.x = 400;
                 background.x -= 5;
                 }
            background.x -=5;
          };

          moveBackground(this.background1);    moveBackground(this.background2);
          moveBackground2(this.background3);
          moveBackground2(this.background4);
          moveBackground3(this.background5);
          moveBackground3(this.background6);
          moveBackground4(this.background7);
          moveBackground4(this.background8);
          moveBackground5(this.background9);
          moveBackground5(this.background10);
          moveBackground5(this.background11);

        if (this.bird.y < 0 || this.bird.y > game.world.height){
            this.restartGame();
          }

        game.physics.arcade.overlap(this.bird, this.pipes, this.hitPipe, null, this);

        // Slowly rotate the bird downward, up to a certain point.
        if (this.bird.angle < 20)
            this.bird.angle += 1;
    },

    jump: function() {

        // If the bird is dead, he can't jump
        if (this.bird.alive == false){
            return;
          }

        this.bird.body.velocity.y = -350;

        // Jump animation
        game.add.tween(this.bird).to({angle: -20}, 100).start();

        // Play sound
        this.jumpSound.play();
    },

    hitPipe: function() {
      var stopBackground = function(background)
      {
        if (background.x > 400) {
             background.x = 0;
             background.x += 5;
             }
        background.x +=5;
      };
      stopBackground(this.background1);
      stopBackground(this.background2);
      stopBackground(this.background3);
      stopBackground(this.background4);
      stopBackground(this.background5);
      stopBackground(this.background6);
      stopBackground(this.background7);
      stopBackground(this.background8);
      stopBackground(this.background9);
      stopBackground(this.background10);
      stopBackground(this.background11);
        // If the bird has already hit a pipe, we have nothing to do
        //513 635
        if (this.bird.alive == false)
            return;

        // Set the alive property of the bird to false
        this.bird.alive = false;

        // Prevent new pipes from appearing
        game.time.events.remove(this.timer);

        // Go through all the pipes, and stop their movement
        this.pipes.forEach(function(p){


            p.body.velocity.x = 0;
            this.deathSound.play();
        }, this);
    },

    restartGame: function() {
        game.state.start('main');
    },

    addOnePipe: function(x, y) {
        var pipe = game.add.sprite(x, y, 'pipe');
        pipe.scale.setTo(0.09746588693, 0.07874015748);
        this.pipes.add(pipe);
        game.physics.arcade.enable(pipe);

        pipe.body.velocity.x = -200;
        pipe.checkWorldBounds = true;
        pipe.outOfBoundsKill = true;
    },

    addRowOfPipes: function() {
        var hole = Math.floor(Math.random()*5)+1;

        for (var i = 0; i < 8; i++)
            if (i != hole && i != hole +1)
                this.addOnePipe(400, i*60+10);

        this.score += 1;
        this.labelScore.text = this.score;
    },
};


game.state.add('main', mainState);
game.state.start('main');
