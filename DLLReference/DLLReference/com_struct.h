#ifndef _COM_STRUCT_
#define _COM_STRUCT_

typedef void(*StartThread)(void*);
typedef void(*EndThread)(void);

typedef	struct	{
	int		nThreadManNum;							/* �X���b�h�Ǘ��ԍ�		*/
	StartThread	StartProc;			/* �X���b�h�֐��A�h���X	*/
	EndThread EndProc;								/* �I���֐��A�h���X		*/
	int		nThreadSize;							/* �i���g�p�j			*/
	int		nStackSize;								/* �X�^�b�N�T�C�Y		*/
	int		nThreadPriority;						/* �X���b�h�D�揇��		*/
} THREAD_INFO_t;

#endif // !_COM_STRUCT_
