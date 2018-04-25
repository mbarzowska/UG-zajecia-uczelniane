#include<stdio.h>

int main() {
  int x = -1;
  int x2 = 0;

  asm volatile (                
      ".intel_syntax noprefix;" 
      "mov eax, %1;"
      "xor ebx, ebx;"           
      "xor ecx, ecx;"          
      "xor edx, edx;"         

    "petla_dodawania:"
      "shl eax;"               
      "jnc reset_wartosci;"    
      "inc ebx;"                

      "cmp ebx, edx;"        

      "jg a1;"            
      "jmp a2;"             

    "a1:"                      
      "mov edx, ebx;"          
      "jmp a2;"               

    "reset_wartosci:"          
      "xor ebx, ebx;"

    "a2:"                      
      "inc ecx;"                
      "cmp ecx, 32;"
      "jnz petla_dodawania;"  
      "mov %0, edx;"          

      ".att_syntax prefix;"
      :"=r" (x2)
      :"r" (x)
      :"eax","ebx","ecx","edx"
  );

  printf("x=%d x2=%d \n", x, x2);

  return 0;
}
