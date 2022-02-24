
    var d = new Date();
    var n = d.getMonth();
    console.log(n);

    if (n == 10) {
        console.log("got here");
        myFunction();
        
    }

function myFunction() {
    console.log("got to function here");
    document.addEventListener('DOMContentLoaded', function () {
        let v = window.innerHeight / 60 + 2,
            h = window.innerWidth / 60 + 2,
            data = {
                'top': h,
                'right': v,
                'bottom': h,
                'left': v
            },
            ul = c = null;
        for (let position in data) {
            c = data[position];
            ul = document.createElement('ul');
            ul.className = 'christmas-lights';
            ul.dataset.position = position;
            for (let i = 0; i <= c; i++) {
                ul.append(document.createElement('li'));
            }
            document.body.append(ul);
        }
    });
}