[CmdletBinding()]
param (
	[switch] $ExecuteNow
)

function ConvertTo-DotFile {
	[CmdletBinding()]
	param (
		[Parameter(Mandatory, ValueFromPipeline)]
		[string[]] $InputFile
	)
	process {
		$InputFile |
			ForEach-Object {
				$currentInputFile = $_
				$currentFilename = [IO.Path]::GetFileNameWithoutExtension($currentInputFile)
				$currentDotFile = $currentFilename + ".dot"

				# "digraph D {",
				# "   rankdir=`"LR`";" | 
				"digraph D {" | Out-File -FilePath $currentDotFile -Encoding ascii

				Get-Content $currentInputFile |
					ForEach-Object {
						if ($_ -match '([A-Z0-9]{1,3})\)([A-Z0-9]{1,3})') {
							$l = $Matches[1]
							$r = $Matches[2]
							"   n_$l -> n_$r"
						}
					} |
					Out-File -FilePath $currentDotFile -Encoding ascii -Append

				"}" | Out-File -FilePath $currentDotFile -Encoding ascii -Append
			}
	}
}

function Invoke-RenderDotFile {
	[CmdletBinding()]
	param (
		[Parameter(Mandatory, ValueFromPipeline)]
		[string[]] $InputFile
	)
	process {
		$dot = 'C:\Program Files (x86)\Graphviz2.38\bin\dot.exe'

		$InputFile |
			ForEach-Object {
				# https://www.graphviz.org/doc/info/command.html
				& $dot $_ -Tpng -O
			}

	}
}