import requests
from bs4 import BeautifulSoup
import time
import csv
from concurrent.futures import ThreadPoolExecutor
import signal
import sys

nfl_team_abbreviations = [
    "crd", "atl", "rav", "buf", "car", "chi", "cin", "cle", "dal", "den",
    "det", "gnb", "htx", "clt", "jax", "kan", "sdg", "ram", "rai", "mia",
    "min", "nwe", "nor", "nyg", "nyj", "phi", "pit", "sfo", "sea", "tam", "oti", "was"
]

AllSeasons = []

for team in nfl_team_abbreviations:
    url = f'https://www.pro-football-reference.com/teams/{team}/'
    response = requests.get(url)
    time.sleep(2)
    if response.status_code == 200:
        soup = BeautifulSoup(response.text, 'html.parser')
        table_element = soup.find('table')
        if table_element:
            tr_elements = table_element.find_all('tr')
            for tr in tr_elements:
                list = []
                th_element = tr.find('th').text
                list.append(th_element)
                td_elements = tr.find_all('td')
                for td in td_elements:
                    if td["data-stat"] == "team":
                        list.append(td.text)
                    if td["data-stat"] == "wins":
                        list.append(td.text)
                    if td["data-stat"] == "losses":
                        list.append(td.text)
                    # if team_abbrev not in list:
                    #     list.append(team_abbrev)
                    if td["data-stat"] == "playoff_result":
                        if td.text != '':
                            list.append(td.text[:1])
                        else:
                            list.append("No")
                AllSeasons.append(list)
        else:
            print("Table with id='franchise_years' not found.")
    else:
        print(f"Request failed with status code {response.status_code}, {url}")


csv_file_path = "NFLSeasons.csv"
with open(csv_file_path, mode='w', newline='') as file:
    writer = csv.writer(file)
    for line in AllSeasons:
        writer.writerow(line)
    file.close()

# csv_file_path = "NFLteams.csv"
# with open(csv_file_path, mode='w', newline='') as file:
#     writer = csv.writer(file)
#     writer.writerow([str("NFL")])
#     for team in nfl_team_abbreviations:
#         list = (abbrev, team)
#         writer.writerow(list)
#     file.close()
