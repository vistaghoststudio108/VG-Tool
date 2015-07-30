#ifndef _COM_STRUCT_
#define _COM_STRUCT_

typedef void(*StartThread)(void*);
typedef void(*EndThread)(void);

typedef	struct	{
	int		nThreadManNum;							/* スレッド管理番号		*/
	StartThread	StartProc;			/* スレッド関数アドレス	*/
	EndThread EndProc;								/* 終了関数アドレス		*/
	int		nThreadSize;							/* （未使用）			*/
	int		nStackSize;								/* スタックサイズ		*/
	int		nThreadPriority;						/* スレッド優先順位		*/
} THREAD_INFO_t;

#endif // !_COM_STRUCT_
