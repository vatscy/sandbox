(define pi 3.141592653589793238462643383279)

(define (square x) (* x x))

(define (sum-of-squares x y) (+ (square x) (square y)))

(define (abs x)
	(cond ((> x 0) x)
		((= x 0) 0)
		((< x 0) (- x))))

(define (abs2 x)
	(cond ((< x 0) (- x))
		(else x)))

(define (abs3 x)
	(if (< x 0)
		(- x)
		x))

(define (>= x y)
	(or (> x y) (= x y)))
