import subprocess

outerr = None
command = ['msiexec', '/i', r'C:\!Personal\PrivateRepo\DeployScript\NaturalnieAppInstaller.msi', '/qn']
# p = subprocess.run(command, stdout=subprocess.PIPE, stderr=subprocess.STDOUT, text=True)

try:
    p = subprocess.check_call(command)
except subprocess.CalledProcessError as e:
    print(e.output)

