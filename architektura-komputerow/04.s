.intel_syntax noprefix
	.text
	.globl _start


_start:
	mov edx, 0                 
	mov ecx, [esp+12]                
 	dec ecx             

zerowanieEDX:
	mov edx, 0              

sprawdzanieZnaku:
	inc ecx
	cmp byte ptr [ecx],0      
	je obliczDlugosc   
                          
	inc edx                   
	cmp byte ptr [ecx],' '     
                             
	jne sprawdzanieZnaku        
	jmp zerowanieEDX         

obliczDlugosc:
  sub ecx,edx                
  mov edx, 0                  

petla:
	inc edx
	cmp byte ptr [ecx+edx-1], 0
	jnz petla                   
	
	mov esi, ecx              
	xor ecx, ecx       
	mov eax, [esp+8]     
	
	mov bl, byte ptr [eax]      
	sub bl, '0'                 
	movzx ebx, bl               
	add ecx, ebx               
	cmp byte ptr [eax+1], 0     
	je przekonwertowane         

	mov bl, byte ptr [eax+1]    
	sub bl, '0'                 
	movzx ebx, bl               
	imul ecx, 10                
                             
	add ecx, ebx                
	cmp byte ptr [eax+2], 0     
	je przekonwertowane         

	mov bl, byte ptr [eax+2]    
	sub bl, '0'                 
	movzx ebx, bl               
	imul ecx, 10               
	add ecx, ebx               


przekonwertowane:            
	mov eax, ecx     
	mov ecx, esi        


podsumowanieiIDrukowanie:
	dec eax
	push eax
	mov eax, 4
	mov ebx, 1
	int 0x80             
	
	pop eax
	cmp eax, 0

	jne podsumowanieiIDrukowanie

	mov eax, 4               
	mov ecx, offset msg      
	int 0x80                 

	mov eax, 1                 
	int 0x80                  

	.data
	msg:	.ascii "\n"
