#ifndef HOOKINGTOOLS_HPP
#define HOOKINGTOOLS_HPP

#include <stdint.h>
#include <Windows.h>

namespace DetoursHook
{
	template<class T>
	T DetourFunction(
		T tSource,
		T tDestination,
		const int iLength
	)
	{
		unsigned char* pTrampolin = (unsigned char*)(
			malloc(
				5 + iLength
			)
			);

		DWORD dwOldProtect;

		VirtualProtect(
			pTrampolin,
			iLength + 5,
			PAGE_EXECUTE_READWRITE,
			&dwOldProtect
		);

		VirtualProtect(
			tSource,
			iLength,
			PAGE_EXECUTE_READWRITE,
			&dwOldProtect
		);

		memcpy(
			pTrampolin,
			tSource,
			iLength
		);

		pTrampolin += iLength;
		pTrampolin[0] = 0xE9;

		*(uintptr_t*)(
			pTrampolin + 1
			) = (uintptr_t)tSource + iLength
			- (uintptr_t)pTrampolin - 5;

		((unsigned char*)tSource)[0] = 0xE9;

		*(uintptr_t*)(
			(uintptr_t)tSource + 1
			) = (uintptr_t)tDestination
			- (uintptr_t)tSource - 5;

		for (
			int i = 5;
			i < iLength;
			i++
			)
			((unsigned char*)tSource)[i] = 0x90;

		VirtualProtect(
			tSource,
			iLength,
			dwOldProtect,
			&dwOldProtect
		);

		return (T)(
			pTrampolin - iLength
			);
	}

	template <class T>
	void RetourFunc(
		T tTrampolin,
		T tOrgFunc,
		const int iLength
	)
	{
		DWORD dwOldProtect;

		VirtualProtect(
			tOrgFunc,
			iLength,
			PAGE_EXECUTE_READWRITE,
			&dwOldProtect
		);

		memcpy(
			tOrgFunc,
			tTrampolin,
			iLength
		);

		VirtualProtect(
			tOrgFunc,
			iLength,
			dwOldProtect,
			&dwOldProtect
		);

		free(
			tTrampolin
		);
	}
};

#endif