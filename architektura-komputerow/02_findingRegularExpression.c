#include <stdio.h>

int main() {
	char *s = "pd ra";    
	int x;                         
 	int y;                             

	asm volatile (
	".intel_syntax noprefix;"
	  "xor eax, eax;"
      "xor ebx, ebx;"              
      "xor ecx, ecx;"

      "mov ebx, %2;"                
      "mov eax, -1;"                  
      "mov ecx, -1;"                   

	"szukaj_p_lub_q:"                   
			"cmp byte ptr [ebx], 0;"        
      "je wyjsciebezA;"                 

      "inc eax;"
      "inc ecx;"                       

      "cmp byte ptr [ebx], 'p';"        
      "je znaleziono_badaj_kolejny;"    
      "cmp byte ptr [ebx], 'q';"       
      "je znaleziono_badaj_kolejny;"    

      "inc ebx;"                       

      "jmp szukaj_p_lub_q;"             
	  
  "znaleziono_badaj_kolejny:"           
      "inc ebx;"                        
      "je szukaj_p_lub_q;"             

      "inc ecx;"

  "szukaj_a:"
      "inc ebx;"                       
      "cmp byte ptr [ebx], 0;"          
      "je wyjsciebezA;"                 

      "inc ecx;"                     

      "cmp byte ptr [ebx], 'a';"      
      "je wyjscie;"
      "jmp szukaj_a;"

  "wyjsciebezA:"
      "mov ecx, ebx;"
      "sub ecx, 1;"

  "wyjscie:"
      "sub ebx, %2;"

      "mov %0, eax;"                   
      "mov %1, ebx;"                   
      ".att_syntax prefix;"
      :"=r" (x), "=r" (y)
      :"r" (s)
      :"al", "eax", "ebx", "ecx"
	); 

	printf("%d, %d \n", x, y);
  return 0;
}
