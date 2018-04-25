.intel_syntax noprefix
	.globl main
	.text

  main:
    xor eax, eax
    xor ebx, ebx

    mov edx, 4

    call funkcja

    push ebx
    push offset msg
    call printf
    add esp, 8

    xor eax, eax
    xor ebx, ebx
    xor edx, edx
    ret

  funkcja:
    xor ebx, ebx
    push edx
    push eax

    cmp edx, 0
    jne j1
    mov ebx, edx
    jmp reset

  j1:
    cmp edx, 1
    jne j2
    mov ebx, 1
    jmp reset

  j2:
    cmp edx, 2
    jne jWiecej
    mov ebx, 0
    jmp reset

  jWiecej:
    push edx
    sub edx, 1
    push ebx
    call funkcja
    pop eax
    add eax, eax
		add ebx, eax
    pop edx

    push edx
    sub edx, 2
    push ebx
    call funkcja
    pop eax
    add ebx, eax
    pop edx

    push edx
    sub edx, 3
    push ebx
    call funkcja
    pop eax
		sub eax, ebx
    mov ebx, eax
    pop edx

  reset:
    	pop eax
			pop ecx
			ret

  .data
  msg:
    .asciz "Wynik funkcji : %d\n"
    .byte 0
