memset 47, 0b1000000
memset 48, 0b1111001
memset 49, 0b0100100
memset 50, 0b0110000
memset 51, 0b0011001
memset 52, 0b0010010
memset 53, 0b0000010
memset 54, 0b1111000
memset 55, 0b0000000
memset 56, 0b0010000
memset 57, 0x008000
memset 58, 0x004000
memset 59, 0x002000
memset 60, 0x001000


lw r11, 57(r0) // slider switch
lw r12, 58(r0) // push button
lw r13, 59(r0) // seven segment display
lw r14, 60(r0) // green leds
lw r4, 0(r11) // slider switch seed
addi r4, r4, 42 // default seed in case slider switch is zero
addi r10, r0, 4 // bits for button that needs to be pressed.

LOOP: lw r3, 0(r12) // load the status of pushbuttons
cmp r10, r3 // want to know if 3 is pushed
beq RANDOM
b LOOP

RANDOM: lw r3, 0(r12) // load the status of pushbuttons
cmp r10, r3 // want to know if 3 is pushed
beq INCREMENT
b DISPLAY

INCREMENT: addi r2, r2, 1
b RANDOM

DISPLAY: add r1, r2, r4
or r1, r1, r2
xor r1, r1, r4
sub r1, r1, r2
and r1, r1, r4
addi r8, r0, 10
addi r7, r0, 10
SUBTRACT: cmp r1, r8
blt WRITE
sub r1, r1, r7
b SUBTRACT

WRITE: lw r7, 47(r1)
sw r7, 0(r13)
sw r1, 0(r14)
b LOOP