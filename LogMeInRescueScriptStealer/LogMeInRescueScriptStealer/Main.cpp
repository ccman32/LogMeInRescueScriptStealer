#include <Windows.h>
#include <stdlib.h>
#include <string.h>  

#include "HackingTools\HookingTools.hpp"

typedef HINSTANCE(__stdcall* tCreateProcessW)(
	_In_opt_    LPCWSTR               lpApplicationName,
	_Inout_opt_ LPWSTR                lpCommandLine,
	_In_opt_    LPSECURITY_ATTRIBUTES lpProcessAttributes,
	_In_opt_    LPSECURITY_ATTRIBUTES lpThreadAttributes,
	_In_        BOOL                  bInheritHandles,
	_In_        DWORD                 dwCreationFlags,
	_In_opt_    LPVOID                lpEnvironment,
	_In_opt_    LPCWSTR               lpCurrentDirectory,
	_In_        LPSTARTUPINFO         lpStartupInfo,
	_Out_       LPPROCESS_INFORMATION lpProcessInformation
	);

tCreateProcessW pOrgCreateProcessW = NULL,
pCreateProcessW = NULL;

HINSTANCE hSelf = NULL;
wchar_t wIniFileName[MAX_PATH];

HINSTANCE __stdcall hkCreateProcessW(
	_In_opt_    LPCWSTR               lpApplicationName,
	_Inout_opt_ LPWSTR                lpCommandLine,
	_In_opt_    LPSECURITY_ATTRIBUTES lpProcessAttributes,
	_In_opt_    LPSECURITY_ATTRIBUTES lpThreadAttributes,
	_In_        BOOL                  bInheritHandles,
	_In_        DWORD                 dwCreationFlags,
	_In_opt_    LPVOID                lpEnvironment,
	_In_opt_    LPCWSTR               lpCurrentDirectory,
	_In_        LPSTARTUPINFO         lpStartupInfo,
	_Out_       LPPROCESS_INFORMATION lpProcessInformation
)
{
	if (
		wcsstr(
			lpCurrentDirectory,
			L"LogMeIn"
		)
		&& wcsstr(
			lpCurrentDirectory,
			L"Script_"
		)
		)
	{
		WIN32_FIND_DATAW findFileData;
		HANDLE hFind;
		wchar_t wlogMeInDirectory[MAX_PATH];

		ZeroMemory(
			wlogMeInDirectory,

			sizeof(
				wlogMeInDirectory
				)
		);

		wcscat_s(
			wlogMeInDirectory,
			lpCurrentDirectory
		);

		wcscat_s(
			wlogMeInDirectory,
			L"\\*"
		);

		hFind = FindFirstFileW(
			wlogMeInDirectory,
			&findFileData
		);

		if (
			hFind != INVALID_HANDLE_VALUE
			)
		{
			WritePrivateProfileStringW(
				L"Settings",
				L"FindFirstFileLastError",
				L"0",
				wIniFileName
			);

			wchar_t wResult[255];

			GetPrivateProfileStringW(
				L"Settings",
				L"OutputPath",
				NULL,
				wResult,

				sizeof(
					wResult
					),

				wIniFileName
			);

			do
			{
				if (
					!(findFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
					)
				{
					wchar_t wOldFileName[MAX_PATH];
					wchar_t wNewFileName[MAX_PATH];

					ZeroMemory(
						wOldFileName,

						sizeof(
							wOldFileName
							)
					);

					ZeroMemory(
						wNewFileName,

						sizeof(
							wNewFileName
							)
					);

					wcscat_s(
						wOldFileName,
						lpCurrentDirectory
					);

					wcscat_s(
						wOldFileName,
						L"\\"
					);

					wcscat_s(
						wOldFileName,
						findFileData.cFileName
					);

					wcscat_s(
						wNewFileName,
						wResult
					);

					wcscat_s(
						wNewFileName,
						L"\\"
					);

					wcscat_s(
						wNewFileName,
						findFileData.cFileName
					);

					if (
						!CopyFileW(
							wOldFileName,
							wNewFileName,
							FALSE)
						)
					{
						wchar_t wLastError[10];
						_itow_s(GetLastError(), wLastError, 10);

						WritePrivateProfileStringW(
							L"Settings",
							L"CopyFileLastError",
							wLastError,
							wIniFileName
						);
					}
					else
					{
						WritePrivateProfileStringW(
							L"Settings",
							L"CopyFileLastError",
							L"0",
							wIniFileName
						);
					}
				}

			} while (
				FindNextFileW(
					hFind,
					&findFileData
				)
				);

			FindClose(hFind);
		}
		else
		{
			wchar_t wLastError[10];
			_itow_s(GetLastError(), wLastError, 10);

			WritePrivateProfileStringW(
				L"Settings",
				L"FindFirstFileLastError",
				wLastError,
				wIniFileName
			);
		}
	}

	return pCreateProcessW(
		lpApplicationName,
		lpCommandLine,
		lpProcessAttributes,
		lpThreadAttributes,
		bInheritHandles,
		dwCreationFlags,
		lpEnvironment,
		lpCurrentDirectory,
		lpStartupInfo,
		lpProcessInformation
	);
}

DWORD WINAPI Initialize(
	void*
)
{
	wchar_t* pCutoff = NULL;

	GetModuleFileNameW(
		hSelf,
		wIniFileName,
		MAX_PATH
	);

	pCutoff = wcsrchr(
		wIniFileName,
		'\\'
	);

	*(pCutoff + 1) = '\0';

	wcscat_s(
		wIniFileName,
		L"LMIRSS.ini"
	);

	HMODULE hKernel32 = NULL;

	while (
		!hKernel32
		)
	{
		hKernel32 = GetModuleHandleA(
			"kernel32.dll"
		);

		if (
			!hKernel32
			)
			Sleep(
				100
			);
	}

	if (
		hKernel32
		)
	{

		pOrgCreateProcessW = (tCreateProcessW)GetProcAddress(
			hKernel32,
			"CreateProcessW"
		);

		if (
			pOrgCreateProcessW
			)
		{
			pCreateProcessW = (tCreateProcessW)DetoursHook::DetourFunction(
				pOrgCreateProcessW,
				&hkCreateProcessW,
				5
			);
		}
	}

	return ERROR_SUCCESS;
}

BOOL WINAPI DllMain(
	HINSTANCE hinstDLL,
	DWORD fdwReason,
	LPVOID
)
{
	if (
		fdwReason == DLL_PROCESS_ATTACH
		)
	{
		wchar_t wProcessFileName[MAX_PATH];

		GetModuleFileNameW(
			NULL,
			wProcessFileName,
			MAX_PATH
		);

		wchar_t* pCutoff = wcsrchr(
			wProcessFileName,
			'\\'
		);

		if (
			pCutoff
			)
		{
			pCutoff += 1;

			if (
				wcscmp(
					pCutoff,
					L"LMI_Rescue.exe"
				)
				&& wcscmp(
					pCutoff,
					L"LMI_Rescue_srv.exe"
				)
				)
				return FALSE;
		}

		hSelf = hinstDLL;

		HANDLE hInitializeThread = CreateThread(
			NULL,
			0,
			&Initialize,
			NULL,
			0,
			NULL
		);

		if (
			hInitializeThread
			)
		{
			CloseHandle(
				hInitializeThread
			);

			return TRUE;
		}
	}
	else if (
		fdwReason == DLL_PROCESS_DETACH
		)
	{
		if (
			pOrgCreateProcessW
			&& pCreateProcessW
			)
		{
			DetoursHook::RetourFunc(
				pCreateProcessW,
				pOrgCreateProcessW,
				5
			);
		}
	}

	return FALSE;
}